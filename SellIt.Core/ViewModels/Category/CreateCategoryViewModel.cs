namespace SellIt.Core.ViewModels.Category
{
    using Microsoft.AspNetCore.Http;

    public class CreateCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IFormFile Image { get; set; }
    }
}
