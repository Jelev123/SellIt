namespace SellIt.Core.ViewModels.ReplayProductMessage
{
    public class ReplyProductMessagesViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int ProductId { get; set; }

        public int ProductMessageId { get; set; }

        public string ProductMessageText { get; set; }

        public string UserId { get; set; }

        public string OwnerUserName { get; set; }

        public string ReplyerUserName { get; set; }

        public int ReplyedMessagesCount { get; set; }
    }
}
