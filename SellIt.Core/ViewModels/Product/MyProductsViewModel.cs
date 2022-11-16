namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;

    public class MyProductsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string CoverPhoto { get; set; }

        public int Viewed { get; set; }

        public int LikedCount { get; set; }

        public bool IsLiked { get; set; }

        public string UserId { get; set; }

        public bool IsAprooved { get; set; }

        public decimal Price { get; set; }
    }
}
