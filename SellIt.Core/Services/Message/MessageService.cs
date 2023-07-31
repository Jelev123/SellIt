namespace SellIt.Core.Services.Message
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Core.ViewModels.Messages;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
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

        public async Task SendMessageAsync(string userName, int id, string message)
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

            await data.Messages.AddAsync(send);
            await data.SaveChangesAsync();
        }

        public async Task ReplyMessageAsync(string replyMessage, string userName, int id)
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

            await data.ReplyMessages.AddAsync(reply);
            await data.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllProductMessagesViewModel>> AllProductMessagesAsync(int id)
             => await this.data.Messages
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
                .OrderByDescending(s => s.Text)
            .ToListAsync();


        public async Task<IEnumerable<AllMessagesViewModel>> AllMessagesAsync()
        {
            string userId = userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            var allMessages = await this.data.Products
             .Where(p => p.CreatedUserId == userId) 
             .SelectMany(p => p.Messages)
             .Where(m => m.UserId == userId || m.Product.CreatedUserId == userId || m.Product.User.Id == userId)
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
      .ToListAsync();

            var currentUserMessages = await this.data.Messages
                .Where(m => m.UserId == userId)
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
                .ToListAsync();

            allMessages.AddRange(currentUserMessages);

            return allMessages;
        }

        public async Task<ProductMessagesById> GetProductMessageByIdAsync(int id)  
            => this.data.Messages
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
    }
}
