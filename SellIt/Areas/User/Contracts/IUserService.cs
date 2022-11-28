namespace SellIt.Areas.User.Contracts
{
    using SellIt.Areas.User.ViewModels;

    public interface IUserService
    {
        Task SendMessage(SendMessageViewModel sendMessage, string userId, string userName);

        IEnumerable<SendMessageViewModel> GetAllProductMessages(int id);
    }
}
