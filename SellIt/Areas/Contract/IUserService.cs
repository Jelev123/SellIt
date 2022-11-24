namespace SellIt.Areas.Contract
{
    using SellIt.Areas.ViewModels;
    using SellIt.Core.ViewModels.ProductMessage;

    public interface IUserService
    {
        IEnumerable<SendMessageViewModel> AllProductMessages(string userId);
    }
}
