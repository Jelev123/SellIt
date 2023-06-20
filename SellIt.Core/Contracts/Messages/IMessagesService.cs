namespace SellIt.Core.Contracts.Messages
{
    using SellIt.Areas.ViewModels;
    using SellIt.Core.ViewModels.Messages;

    public interface IMessagesService
    {
        Task SendMessage(string userName, int id, string message);
        Task ReplyMessage(string replyMessage, string userName, int id);
        IEnumerable<AllProductMessagesViewModel> AllProductMessages(int id);
        IEnumerable<AllMessagesViewModel> AllMessages();
        ProductMessagesById GetProductMessageById(int id);
    }
}
