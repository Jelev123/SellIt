namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;

    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? CategoryName { get; set; }

        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        public string CoverPhoto { get; set; }

        public int Viewed { get; set; }

        public int LikedCount { get; set; }

        public bool IsLiked { get; set; }

        public string UserId { get; set; }

        public bool IsAprooved { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

    }
}
