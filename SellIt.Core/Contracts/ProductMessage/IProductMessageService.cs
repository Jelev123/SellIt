namespace SellIt.Core.Contracts.ProductMessage
{
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.ViewModels.ProductMessage;

    public interface IProductMessageService
    {
        Task SendMessage(SendMessageViewModel sendMessage, string userId, int id);

        IEnumerable<SendMessageViewModel> AllMessages(int id, string userId);
    }
}
