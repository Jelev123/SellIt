namespace SellIt.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AuthCredentials
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
