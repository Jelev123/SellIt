namespace SellIt.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
