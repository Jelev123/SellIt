namespace SellIt.Core.ViewModels.Product
{
    public class IndexRandomViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string CoverPhoto { get; set; }

        public bool IsLiked { get; set; }

        public decimal Price { get; set; }

        public bool IsAproved { get; set; }

        public int LikedCount { get; set; }

    }
}
