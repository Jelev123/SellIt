namespace SellIt.Core.Contracts.Messages
{
    using SellIt.Areas.ViewModels;
    using SellIt.Core.ViewModels.Messages;

    public interface IMessagesService
    {
        Task SendMessage(SendMessageViewModel sendMessage, string userId, string userName, int id);
        Task ReplyMessage(ReplyMessageViewModel replyMessage, string userId, string userName, int id);
        IEnumerable<AllProductMessagesViewModel> AllProductMessages(int id);
        IEnumerable<AllMessagesViewModel> AllMessages(string userId);
        ProductMessagesById GetProductMessageById(int id);
    }
}
