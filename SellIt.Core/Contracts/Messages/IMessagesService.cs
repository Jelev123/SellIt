namespace SellIt.Core.Contracts.Messages
{
    using SellIt.Core.ViewModels.Messages;

    public interface IMessagesService
    {
        IEnumerable<AllProductMessagesViewModel> AllProductMessages(int id);
    }
}
