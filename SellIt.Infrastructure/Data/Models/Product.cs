namespace SellIt.Infrastructure.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Viewed { get; set; }

        public int Liked { get; set; }

        public int ClickCounter { get; set; }

        public bool IsAproved { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string? UserId { get; set; }

        public User? User { get; set; }

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public ICollection<Image> Images { get; set; } = new HashSet<Image>();

        public ICollection<ProductMessages> ProductMessages { get; set; } = new HashSet<ProductMessages>();
    }
}
