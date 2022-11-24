namespace SellIt.Core.Contracts.ReplyProductMessage
{
    using SellIt.Core.ViewModels.ProductMessage;
    using SellIt.Core.ViewModels.ReplayProductMessage;

    public interface IReplyProductMessageService
    {
        Task ReplyMessage(ReplyProductMessagesViewModel replyProductMessage, string userId);

        IEnumerable<ReplyProductMessagesViewModel> AllReplyedMessages(int id);


    }
}
