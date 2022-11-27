namespace SellIt.Core.ViewModels.User
{
    using SellIt.Core.ViewModels.ProductMessage;
    using SellIt.Core.ViewModels.ReplayProductMessage;

    public class AllUserProductMessages
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int Count { get; set; }

        public int ReplyedProductMessagesCount { get; set; }

        public int ProductMessageId { get; set; }

        public List<ReplyProductMessagesViewModel> ReplyProductMessages { get; set; }
        public List<SendMessageViewModel> Messages { get; set; }
    }
}
