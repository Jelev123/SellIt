namespace SellIt.Core.Services.Message
{
    using SellIt.Areas.ViewModels;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Core.ViewModels.Messages;
    using SellIt.Core.ViewModels.ReplyMessages;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class MessageService : IMessagesService
    {
        private readonly ApplicationDbContext data;

        public MessageService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public Task SendMessage(SendMessageViewModel sendMessage, string userId, string userName, int id)
        {
            var send = new Message
            {
                Text = sendMessage.Text,
                ProductId = id,
                UserId = userId,
                UserName = userName,
                Date = DateTime.UtcNow,
            };
            data.Messages.Add(send);
            data.SaveChanges();
            return Task.CompletedTask;
        }

        public Task ReplyMessage(SendMessageViewModel sendMessage, string userId, string userName, int id)
        {
            var reply = new ReplyMessage
            {
                MessageId = id,
                ReplyText = sendMessage.Replytext,
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
                    ReplyMessages = s.ReplyMessages.Select(s => new AllReplyProductMessagesViewModel
                    {
                        ReplyerName = s.ReplyerUser.UserName,
                        ReplyText = s.ReplyText,
                        ProductName = s.Message.Product.Name,
                        Date = s.Date,
                    }).ToList()
                })
                .OrderByDescending(s => s.Text);
            return all;
        }

        public IEnumerable<SendMessageViewModel> AllMessages(string userId)
        {
            var allMessages = from M in data.Messages
                              join RM in data.ReplyMessages
                              on M.Id equals RM.MessageId into joined
                              from j in joined.DefaultIfEmpty()
                              where M.UserId == userId || j.ReplyerUserId == userId
                              select new SendMessageViewModel
                              {
                                  Id = M.Id,
                                  Text = M.Text,
                                  ProductName = M.Product.Name,
                                  UserName = M.UserName,
                                  Date = M.Date,
                                  ReplyMessages = M.ReplyMessages
                                  .Select(s => new AllReplyProductMessagesViewModel
                                  {
                                      Id = j.Id,
                                      MessageId = j.MessageId,
                                      ReplyText = j.ReplyText,
                                      ReplyerName = j.ReplayerUserName,
                                      Date = j.Date,
                                  }).ToList(),
                              };
            return allMessages;
        }

        public SendMessageViewModel GetProductMessageById(int id)
        {
            var allMessages = this.data.Messages
                 .Where(s => s.Id == id)
                               .Select(s => new SendMessageViewModel
                               {
                                   Id = s.Id,
                                   Text = s.Text,
                                   ProductName = s.Product.Name,
                                   ProductId = s.ProductId,
                                   UserName = s.UserName,
                                   ReplyMessages = s.ReplyMessages.Select(s => new AllReplyProductMessagesViewModel
                                   {
                                       ReplyerName = s.ReplyerUser.UserName,
                                       ReplyText = s.ReplyText,
                                       Date = s.Date,
                                       ProductName = s.Message.Product.Name,
                                   }).ToList()
                               }).FirstOrDefault();
            return allMessages;
        }

        public SendMessageViewModel GetMessageByUserId(string userId, int id)
        {
            var allMessages = (from M in data.Messages
                               join RM in data.ReplyMessages
                               on M.Id equals RM.MessageId into joined
                               from j in joined.DefaultIfEmpty()
                               where M.UserId == userId || j.ReplyerUserId == userId
                               select new SendMessageViewModel
                               {
                                   Id = id,
                                   Text = M.Text,
                                   ProductName = M.Product.Name,
                                   ProductId = M.ProductId,
                                   UserName = M.UserName,
                                   Date = M.Date,
                                   ReplyMessages = M.ReplyMessages
                                   .Select(s => new AllReplyProductMessagesViewModel
                                   {
                                       ReplyText = j.ReplyText,
                                       ReplyerName = j.ReplayerUserName,
                                       Date = j.Date,
                                   }).ToList(),
                               }).FirstOrDefault();
            return allMessages;
        }
    }
}
