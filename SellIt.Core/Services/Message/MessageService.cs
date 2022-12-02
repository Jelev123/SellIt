namespace SellIt.Core.Services.Message
{
    using SellIt.Areas.ViewModels;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Core.ViewModels.Messages;
    using SellIt.Core.ViewModels.ReplyMessages;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;

    public class MessageService : IMessagesService
    {
        private readonly ApplicationDbContext data;

        public MessageService(ApplicationDbContext data)
        {
            this.data = data;
        }


        public Task ReplyMessage(SendMessageViewModel sendMessage, string userId, string userName, int id)
        {
            var reply = new ReplyMessage
            {
                MessageId = id,
                ReplyText = sendMessage.Replytext,
                Date = DateTime.UtcNow,
                ReplyerUserId = userId,
            };

            data.ReplyMessages.Add(reply);
            data.SaveChanges();
            return Task.CompletedTask;
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
                    }).ToList()
                });

            return all;
        }

        public IEnumerable<SendMessageViewModel> AllMessages(string userId)
        {
            var allMessages = this.data.Messages
                .Where(s => s.UserId == userId)
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
                                  }).ToList()
                              });

            return allMessages;
        }
    }
}
