namespace SellIt.Controllers.ForAprooved
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.ForAprooved;

    public class ForAproovedController : Controller
    {
        private readonly IForAproovedService aproovedService;

        public ForAproovedController(IForAproovedService aproovedService)
        {
            this.aproovedService = aproovedService;
        }

        public IActionResult GetAllProductsForAproove() => View(this.aproovedService.GetAllProductsForAproove());

        public IActionResult SetAproove(int id)
        {
            this.aproovedService.SetAproove(id);
            return this.Redirect("/");
        }
    }
}
