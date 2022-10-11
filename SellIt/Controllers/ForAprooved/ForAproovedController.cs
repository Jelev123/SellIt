namespace SellIt.Controllers.ForAprooved
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.ForAprooved;
    using SellIt.Core.ViewModels.Product;

    public class ForAproovedController : Controller
    {
        private readonly IForAproovedService aproovedService;

        public ForAproovedController(IForAproovedService aproovedService)
        {
            this.aproovedService = aproovedService;
        }

        public IActionResult AllProducts()
        {
            
            var allProducts = this.aproovedService.GetAllProducts();
            return View(allProducts);
        }


    }
}
