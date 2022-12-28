namespace SellIt.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string? RoleId { get; set; }

        public IdentityRole Role { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
