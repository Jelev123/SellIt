namespace SellIt.Core.Services.ReplyProductMessage
{
    using SellIt.Core.Contracts.ReplyProductMessage;
    using SellIt.Core.ViewModels.ReplayProductMessage;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ReplyProductMessageService : IReplyProductMessageService
    {
        private readonly ApplicationDbContext data;

        public ReplyProductMessageService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public Task ReplyMessage(ReplyProductMessagesViewModel replyMessage, string userId)
        {
            var reply = new ReplyProductMessage
            {
                ProductMessagesId = replyMessage.ProductMessageId == 0 ? replyMessage.Id : replyMessage.ProductMessageId,
                Text = replyMessage.Text,
                UserId = userId,
            };
            this.data.Add(reply);
            this.data.SaveChanges();
            return Task.CompletedTask;
        }

        public IEnumerable<ReplyProductMessagesViewModel> AllReplyedMessages(int id)
        {
            var allReplyedMessages = this.data.ReplyProductMessages
                .Where(s => s.ProductMessagesId == id)
                .Select(s => new ReplyProductMessagesViewModel
                {
                    ProductMessageId = id,
                    Text = s.Text,
                    UserId = s.UserId,
                    OwnerUserName = s.User.UserName,
                    ReplyerUserName = s.ProductMessages.User.UserName,
                    ProductMessageText = s.ProductMessages.Text,
                });

            return allReplyedMessages;
        }
    }
}
