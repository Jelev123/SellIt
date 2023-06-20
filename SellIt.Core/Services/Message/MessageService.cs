namespace SellIt.Core.Services.Message
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Core.ViewModels.Messages;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class MessageService : IMessagesService
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public MessageService(ApplicationDbContext data, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.data = data;
            this.userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task SendMessage(string userName, int id, string message)
        {
            string userId = userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            var send = new Message
            {
                Text = message,
                ProductId = id,
                UserId = userId,
                UserName = userName,
                Date = DateTime.UtcNow,
            };

            data.Messages.Add(send);
            data.SaveChanges();
            return Task.CompletedTask;
        }

        public Task ReplyMessage(string replyMessage, string userName, int id)
        {
            string userId = userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            var reply = new ReplyMessage
            {
                MessageId = id,
                ReplyText = replyMessage,
                Date = DateTime.UtcNow,
                ReplyerUserId = userId,
                ReplayerUserName = userName,
            };
            data.ReplyMessages.Add(reply);
            data.SaveChanges();
            return Task.CompletedTask;
        }

        public IEnumerable<AllProductMessagesViewModel> AllProductMessages(int id)
        {
            var all = this.data.Messages
                .Where(s => s.ProductId == id)
                .Select(s => new AllProductMessagesViewModel
                {
                    Id = s.Id,
                    Text = s.Text,
                    UserId = s.UserId,
                    UserName = s.UserName,
                    Date = s.Date,
                    ProductName = s.Product.Name,
                    Image = s.Product.Images.Select(s => s.URL).FirstOrDefault(),
                    ReplyMessages = s.ReplyMessages.Select(s => new AllReplyMessagesViewModel
                    {
                        ReplyerUserName = s.ReplyerUser.UserName,
                        ReplyText = s.ReplyText,
                        ProductName = s.Message.Product.Name,
                        Date = s.Date,
                    }).ToList()
                })
                .OrderByDescending(s => s.Text);
            return all;
        }

        public IEnumerable<AllMessagesViewModel> AllMessages()
        {
            string userId = userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            var allMessages = this.data.Products
             .Where(p => p.CreatedUserId == userId) // Filter products owned by the current user
             .SelectMany(p => p.Messages) // Retrieve messages for the user's products
             .Where(m => m.UserId == userId || m.Product.CreatedUserId == userId || m.Product.User.Id == userId) // Include messages sent by the current user, related to their product, or sent to other users' products
             .Select(m => new AllMessagesViewModel
             {
                 Id = m.Id,
                 Text = m.Text,
                 UserName = m.UserName,
                 Date = m.Date,
                 ProductId = m.ProductId,
                 ProductName = m.Product.Name,
                 Photo = m.Product.Images.FirstOrDefault().URL,
                 ReplyMessages = m.ReplyMessages
              .Where(r => r.ReplyerUserId == userId || r.Message.UserId == userId)
              .Select(r => new ReplyMessageViewModel
              {
                  ReplyText = r.ReplyText,
                  ReplyerUserName = r.ReplayerUserName,
                  ReplyerDate = r.Date
              })
              .ToList()
             })
      .ToList();

            var currentUserMessages = this.data.Messages
                .Where(m => m.UserId == userId /*&& !this.data.Products.Any(p => p.Messages.Contains(m))*/) // Include messages sent by the current user to other users' products
                .Select(m => new AllMessagesViewModel
                {
                    Id = m.Id,
                    Text = m.Text,
                    UserName = m.UserName,
                    Date = m.Date,
                    ProductId = m.ProductId,
                    ProductName = m.Product.Name,
                    Photo = m.Product.Images.FirstOrDefault().URL,
                    ReplyMessages = m.ReplyMessages
                        .Where(r => r.ReplyerUserId == userId || r.Message.UserId == userId)
                        .Select(r => new ReplyMessageViewModel
                        {
                            ReplyText = r.ReplyText,
                            ReplyerUserName = r.ReplayerUserName,
                            ReplyerDate = r.Date
                        })
                        .ToList()
                })
                .ToList();

            allMessages.AddRange(currentUserMessages);

            return allMessages;



        }

        public ProductMessagesById GetProductMessageById(int id)
        {
            var allMessages = this.data.Messages
                 .Where(s => s.Id == id)
                               .Select(s => new ProductMessagesById
                               {
                                   Id = s.Id,
                                   Text = s.Text,
                                   ProductName = s.Product.Name,
                                   ProductId = s.ProductId,
                                   UserName = s.UserName,
                                   Image = s.Product.Images.Select(s => s.URL).FirstOrDefault(),
                                   ReplyMessages = s.ReplyMessages.Select(s => new AllReplyMessagesViewModel
                                   {
                                       ReplyerUserName = s.ReplyerUser.UserName,
                                       ReplyText = s.ReplyText,
                                       Date = s.Date,
                                       ProductName = s.Message.Product.Name,
                                   }).ToList(),
                               }).FirstOrDefault();
            return allMessages;
        }
    }
}
