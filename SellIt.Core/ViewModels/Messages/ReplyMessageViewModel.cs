namespace SellIt.Core.ViewModels.Messages
{
    public class ReplyMessageViewModel
    {
        public int Id { get; set; }

        public string ReplyText { get; set; }

        public string ReplyerUserName { get; set; }

        public DateTime ReplyerDate { get; set; } = DateTime.UtcNow;
    }
}
