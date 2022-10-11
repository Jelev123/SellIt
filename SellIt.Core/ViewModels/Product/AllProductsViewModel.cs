namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;

    public class AllProductsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? CategoryName { get; set; }

        public string Image { get; set; }
    }
}
