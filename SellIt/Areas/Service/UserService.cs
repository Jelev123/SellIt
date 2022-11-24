namespace SellIt.Areas.Service
{
    using SellIt.Areas.Contract;
    using SellIt.Core.ViewModels.ProductMessage;
    using SellIt.Core.ViewModels.ReplayProductMessage;
    using SellIt.Infrastructure.Data;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<SendMessageViewModel> AllProductMessages(string userId)
        {
            var allMessages = this.data.ProductMessages
                .Where(x => x.UserId == userId)
                .Select(s => new SendMessageViewModel
                {
                    Id = s.Id,
                    Text = s.Text,
                    UserName = s.User.UserName,
                    ProductId = s.ProductId,
                    ProductName = s.Product.Name,
                    ProductImage = s.Product.Images.Select(s => s.URL).FirstOrDefault(),
                    ReplyProductMessages = s.ReplyProductMessages.Select(s => new ReplyProductMessagesViewModel
                    {
                        ProductMessageText = s.Text,
                        ReplyerUserName = s.User.UserName
                    }).ToList(),
                });

            return allMessages;
        }
    }
}
