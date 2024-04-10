namespace SellIt.Core.Services.Message
{
    using Microsoft.EntityFrameworkCore;
    using SellIt.Core.Constants.Error;
    using SellIt.Core.Contracts.Admin;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Core.Contracts.User;
    using SellIt.Core.Handlers.Error;
    using SellIt.Core.Repository;
    using SellIt.Core.ViewModels.Messages;
    using SellIt.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Linq;


    public class MessageService : IMessagesService
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Message> messageRepository;
        private readonly IRepository<ReplyMessage> replyMessageRepository;
        private readonly IAdminService adminService;
        private readonly string CurrentUserId;

        public MessageService(IAdminService adminService,
            IRepository<Product> productRepository,
            IRepository<Message> messageRepository,
            IRepository<ReplyMessage> replyMessageRepository)
        {
            this.adminService = adminService;
            CurrentUserId = adminService.CurrentUserAccessor();
            this.productRepository = productRepository;
            this.messageRepository = messageRepository;
            this.replyMessageRepository = replyMessageRepository;
        }

        public async Task SendMessageAsync(string userName, int id, string message)
        {
            var send = new Message
            {
                Text = message,
                ProductId = id,
                UserId = CurrentUserId,
                UserName = userName,
                Date = DateTime.UtcNow,
            };

            await messageRepository.AddAsync(send);
            await messageRepository.SaveChangesAsync();
        }

        public async Task ReplyMessageAsync(string replyMessage, string userName, int id)
        {
            var reply = new ReplyMessage
            {
                MessageId = id,
                ReplyText = replyMessage,
                Date = DateTime.UtcNow,
                ReplyerUserId = CurrentUserId,
                ReplayerUserName = userName,
            };

            await replyMessageRepository.AddAsync(reply);
            await replyMessageRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllProductMessagesViewModel>> AllProductMessagesAsync(int id)
        {
            return await messageRepository.AllAsNoTracking()
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
        }

        public async Task<IEnumerable<AllMessagesViewModel>> AllMessagesAsync()
        {

            var allMessages = await GetAllProductMessagesAsync();

            var currentUserMessages = await GetAllCurrentUserMessagesAsync();

            allMessages.Concat(currentUserMessages);

            return allMessages;
        }

        public async Task<ProductMessagesById> GetProductMessageByIdAsync(int id)
        {
            var message = await messageRepository.AllAsNoTracking()
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
                               }).FirstOrDefaultAsync()
                               ?? throw new DataNotFoundException(string.Format(
                                            ErrorMessages.DataDoesNotExist,
                                            typeof(Product).Name, "id", id));

            return message;
        }

        private async Task<IEnumerable<AllMessagesViewModel>> GetAllProductMessagesAsync()
        {
            var allMessages = await productRepository.All()
            .Where(p => p.CreatedUserId == CurrentUserId)
            .SelectMany(p => p.Messages)
            .Where(m => m.UserId == CurrentUserId || m.Product.CreatedUserId == CurrentUserId || m.Product.User.Id == CurrentUserId)
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
             .Where(r => r.ReplyerUserId == CurrentUserId || r.Message.UserId == CurrentUserId)
             .Select(r => new ReplyMessageViewModel
             {
                 ReplyText = r.ReplyText,
                 ReplyerUserName = r.ReplayerUserName,
                 ReplyerDate = r.Date
             })
             .ToList()
            })
             .ToListAsync();

            return allMessages;
        }

        private async Task<IEnumerable<AllMessagesViewModel>> GetAllCurrentUserMessagesAsync()
        {
            var currentUserMessages = await messageRepository.All()
               .Where(m => m.UserId == CurrentUserId)
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
                       .Where(r => r.ReplyerUserId == CurrentUserId || r.Message.UserId == CurrentUserId)
                       .Select(r => new ReplyMessageViewModel
                       {
                           ReplyText = r.ReplyText,
                           ReplyerUserName = r.ReplayerUserName,
                           ReplyerDate = r.Date
                       })
                       .ToList()
               })
               .ToListAsync();

            return currentUserMessages;
        }
    }
}
