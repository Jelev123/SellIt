namespace SellIt.Core.Services.Message
{
    using SellIt.Core.Contracts.Messages;
    using SellIt.Core.ViewModels.Messages;
    using SellIt.Core.ViewModels.ReplyMessages;
    using SellIt.Infrastructure.Data;
    using System.Collections.Generic;

    public class MessageService : IMessagesService
    {
        private readonly ApplicationDbContext data;

        public MessageService(ApplicationDbContext data)
        {
            this.data = data;
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
    }
}
