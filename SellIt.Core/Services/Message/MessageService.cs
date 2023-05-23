namespace SellIt.Core.Services.Message
{
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
        private readonly ICountService countService;

        public MessageService(ApplicationDbContext data, ICountService countService)
        {
            this.data = data;
            this.countService = countService;
        }

        public Task SendMessage(string userId, string userName, int id, string message)
        {
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

        public Task ReplyMessage(string replyMessage, string userId, string userName, int id)
        {
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

        public IEnumerable<AllMessagesViewModel> AllMessages(string userId)
        {
            var allMessages = this.data.Messages
    .Where(m => m.UserId == userId || this.data.ReplyMessages.Any(r => r.ReplyerUserId == userId && r.MessageId == m.Id))
    .Select(m => new AllMessagesViewModel
    {
        Id = m.Id,
        Text = m.Text,
        UserName = m.UserName,
        Date = m.Date,
        ProductName = m.Product.Name,
        ReplyMessages = this.data.ReplyMessages
            .Where(r => r.MessageId == m.Id && (r.ReplyerUserId == userId || r.Message.UserId == userId))
            .Select(r => new ReplyMessageViewModel
            {
                ReplyText = r.ReplyText,
                ReplyerUserName = r.ReplayerUserName,
                ReplyerDate = r.Date
            })
            .ToList()
    })
    .ToList();

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
