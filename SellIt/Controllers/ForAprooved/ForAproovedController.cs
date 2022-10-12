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

        public IActionResult GetAllProductsForAproove()
        {
            
            var allProducts = this.aproovedService.GetAllProductsForAproove();
            return View(allProducts);
        }

        public IActionResult SetAproove(int id)
        {
            this.aproovedService.SetAproove(id);
            return this.Redirect("/");
        }


    }
}
