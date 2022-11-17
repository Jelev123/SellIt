namespace SellIt.Core.Contracts.ProductMessage
{
    using SellIt.Core.ViewModels.ProductMessage;

    public interface IProductMessageService
    {
        Task SendMessage(SendMessageViewModel sendMessage, string userId, int id);

        IEnumerable<SendMessageViewModel> AllMessages(int id);
    }
}
