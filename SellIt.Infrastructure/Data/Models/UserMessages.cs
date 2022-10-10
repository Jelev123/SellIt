namespace SellIt.Infrastructure.Data.Models
{
    public class UserMessages
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        public User User { get; set; }
    }
}
