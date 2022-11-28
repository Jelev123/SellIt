namespace SellIt.Areas.User.Services
{
    using SellIt.Areas.User.Contracts;
    using SellIt.Areas.User.ViewModels;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public Task SendMessage(SendMessageViewModel sendMessage, string userId, string userName)
        {
            var send = new Message
            {
                Text = sendMessage.Text,
                ProductId = sendMessage.Id,
                UserName = userName,
                UserId = userId,
                Date = sendMessage.Date,
            };

            var allMessage = new AllMessages
            {
                MessageId = send.Id,
                UserSenderId = send.UserId,
                Message = send,
            };


            data.Messages.Add(send);
            data.AllMessages.Add(allMessage);
            data.SaveChanges();
            return Task.CompletedTask;
        }


        public IEnumerable<SendMessageViewModel> GetAllProductMessages(int id)
        {
            var allMessages = data.Messages
                .Where(s => s.ProductId == id)
                .Select(s => new SendMessageViewModel
                {
                    Text = s.Text,
                    UserId = s.UserId,
                    UserName = s.UserName,
                    Date = s.Date,
                });

            return allMessages;
        }
    }
}
