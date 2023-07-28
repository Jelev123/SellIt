namespace SellIt.Core.Contracts.Messages
{
    using SellIt.Areas.ViewModels;
    using SellIt.Core.ViewModels.Messages;

    public interface IMessagesService
    {
        Task SendMessageAsync(string userName, int id, string message);
        Task ReplyMessageAsync(string replyMessage, string userName, int id);
        Task<IEnumerable<AllProductMessagesViewModel>> AllProductMessagesAsync(int id);
        Task<IEnumerable<AllMessagesViewModel>> AllMessagesAsync();
        Task<ProductMessagesById> GetProductMessageByIdAsync(int id);
    }
}
