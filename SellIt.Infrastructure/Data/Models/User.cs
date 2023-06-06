namespace SellIt.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime DateCreated { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
