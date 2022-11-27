namespace SellIt.Areas.Service
{
    using Microsoft.EntityFrameworkCore;
    using SellIt.Areas.Contract;
    using SellIt.Core.ViewModels.ProductMessage;
    using SellIt.Core.ViewModels.ReplayProductMessage;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<SendMessageViewModel> AllProductUserMessages(string userId)
        {

          
            var test = (from r in data.ReplyProductMessages
                        join p in data.ProductMessages
                        on r.ProductMessagesId equals p.Id
                        select new SendMessageViewModel
                        {
                            ProductMessageId = r.ProductMessagesId == null ? p.Id : r.ProductMessagesId,
                            Text = p.Text,
                            UserName = p.User.UserName,
                            ProductId = p.ProductId,
                            ProductName = p.Product.Name,
                            ProductImage = p.Product.Images.Select(s => s.URL).FirstOrDefault(),
                            ReplyProductMessages = p.ReplyProductMessages.Select(s => new ReplyProductMessagesViewModel
                            {
                                ProductMessageId = p.Id,
                                Id = s.Id,
                                ProductMessageText = r.Text,
                                ReplyerUserName = r.User.UserName,
                            }).ToList(),
                        }).ToList();
            
           
            var allMessages = this.data.ProductMessages
                .Where(s => s.UserId == userId)
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
                })
                ;
            return allMessages;
        }
    }
}
