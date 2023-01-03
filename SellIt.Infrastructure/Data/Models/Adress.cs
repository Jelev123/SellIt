namespace SellIt.Infrastructure.Data.Models
{
    public class Adress
    {
        public string Id { get; set; }

        public string? Road { get; set; }

        public string? Suburb { get; set; }

        public string? City { get; set; }

        public string? StateDistrict { get; set; }

        public string? State { get; set; }

        public string? PostCode { get; set; }

        public string? Country { get; set; }

        public string? CountryCode { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
