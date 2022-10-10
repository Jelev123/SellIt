namespace SellIt.Infrastructure.Data.Models
{
    public class ProductMessages
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
