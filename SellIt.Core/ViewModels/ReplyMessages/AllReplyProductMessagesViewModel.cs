namespace SellIt.Core.ViewModels.ReplyMessages
{
    public class AllReplyProductMessagesViewModel
    {
        public int Id { get; set; }

        public string ReplyerName { get; set; }

        public string ReplyText { get; set; }

        public string ProductName { get; set; }

        public int MessageId { get; set; }
        public DateTime Date { get; set; }
    }
}
