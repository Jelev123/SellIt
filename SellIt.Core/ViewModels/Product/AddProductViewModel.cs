namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;

    public class AddProductViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string? CategoryName { get; set; }

        public IFormFile Image { get; set; }

    }
}
