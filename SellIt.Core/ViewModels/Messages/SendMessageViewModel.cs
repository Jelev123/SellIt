namespace SellIt.Areas.ViewModels
{
    using SellIt.Core.ViewModels.ReplyMessages;

    public class SendMessageViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Replytext { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string ReplayerUserName { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public List<AllReplyProductMessagesViewModel> ReplyMessages { get; set; }


    }
}
