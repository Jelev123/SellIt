namespace SellIt.Areas.User.ViewModels
{
    public class SendMessageViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int ProductId { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
