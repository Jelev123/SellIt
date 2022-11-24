namespace SellIt.Infrastructure.Data.Models
{
    public class ReplyProductMessage
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int ProductMessagesId { get; set; }

        public ProductMessages ProductMessages { get; set; }

        public string? UserId { get; set; }

        public User User { get; set; }
    }
}
