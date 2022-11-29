namespace SellIt.Areas.User.Contracts
{
    using SellIt.Areas.ViewModels;

    public interface IUserService
    {
        Task SendMessage(SendMessageViewModel sendMessage, string userId, string userName, int id);
        Task ReplyMessage(SendMessageViewModel sendMessage, string userId, string userName, int id);
    }
}
