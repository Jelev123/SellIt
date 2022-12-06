namespace SellIt.Core.ViewModels.Messages
{
    public class AllMessagesViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string UserName { get; set; }

        public string ProductName { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public List<AllReplyMessagesViewModel> ReplyMessages { get; set; }
    }
}
