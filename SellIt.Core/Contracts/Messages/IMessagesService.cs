namespace SellIt.Core.Contracts.Messages
{
    using SellIt.Areas.ViewModels;
    using SellIt.Core.ViewModels.Messages;

    public interface IMessagesService
    {
        Task SendMessage(SendMessageViewModel sendMessage, string userId, string userName, int id);
        Task ReplyMessage(SendMessageViewModel sendMessage, string userId, string userName, int id);
        IEnumerable<AllProductMessagesViewModel> AllProductMessages(int id);

        IEnumerable<SendMessageViewModel> AllMessages(string userId);
    }
}
