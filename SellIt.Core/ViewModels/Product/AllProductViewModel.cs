namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;

    public class AllProductViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public string CoverPhoto { get; set; }

        public string UserId { get; set; }

        public decimal Price { get; set; }
    }
}
