namespace SellIt.Infrastructure.Data.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Viewed { get; set; }

        public int LikedCount { get; set; }

        public decimal Price { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsAproved { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string? CreatedUserId { get; set; }

        public User? User { get; set; }

        public string ProductAdress { get; set; }

        public ICollection<LikedProduct> LikedProducts { get; set; } = new HashSet<LikedProduct>();

        public ICollection<Image> Images { get; set; } = new HashSet<Image>();

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
