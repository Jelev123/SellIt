namespace SellIt.Core.ViewModels.Messages
{
    public class ProductMessagesById
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string UserName { get; set; }

        public string ReplayerUserName { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string Image { get; set; }

        public List<AllReplyMessagesViewModel> ReplyMessages { get; set; }
    }
}
