namespace SellIt.Core.ViewModels.Messages
{
    public class AllReplyMessagesViewModel
    {
        public int Id { get; set; }

        public int MessageId { get; set; }

        public string ReplyText { get; set; }

        public string ReplyerUserName { get; set; }

        public DateTime Date { get; set; }

        public string ProductName { get; set; }
    }
}
