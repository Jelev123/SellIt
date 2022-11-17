namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.ViewModels.ProductMessage;

    public class GetByIdAndLikeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        public int Viewed { get; set; }

        public int LikedCount { get; set; }

        public bool IsLiked { get; set; }

        public string UserId { get; set; }

        public bool IsAprooved { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public List<SendMessageViewModel> SendMesasge { get; set; }
    }
}
