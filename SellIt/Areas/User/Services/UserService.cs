namespace SellIt.Areas.User.Services
{
    using SellIt.Areas.User.Contracts;
    using SellIt.Areas.ViewModels;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
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
    }
}
