namespace SellIt.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Message
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
