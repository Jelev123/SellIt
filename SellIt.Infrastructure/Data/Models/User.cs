namespace SellIt.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public ICollection<UserMessages> Messages { get; set; } = new HashSet<UserMessages>();

        public ICollection<ProductMessages> ProductMessages { get; set; } = new HashSet<ProductMessages>();
    }
}
