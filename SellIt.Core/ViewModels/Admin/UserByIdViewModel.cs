namespace SellIt.Core.ViewModels.Admin
{
    public class UserByIdViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string RoleName { get; set; }

        public string Email { get; set; }

        public DateTime DateCreated { get; set; }

        public string ProductName { get; set; }

        public int ProductsCount { get; set; }
    }
}
