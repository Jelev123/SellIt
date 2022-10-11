namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.ViewModels.Image;

    public class AllProductsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? CategoryName { get; set; }

        public List<GalleryModel> Image { get; set; }
    }
}
