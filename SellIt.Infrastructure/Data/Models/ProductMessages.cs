namespace SellIt.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ProductMessages
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Count { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<ReplyProductMessage> ReplyProductMessages { get; set; } = new HashSet<ReplyProductMessage>();
    }
}
