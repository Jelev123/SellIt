namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    public class SearchViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string CoverPhoto { get; set; }

        public string UserId { get; set; }

        public decimal Price { get; set; }
    }
}
