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

        public async Task<IActionResult> GetAllProductsForAproove()
            => View(await this.aproovedService.GetAllProductsForAprooveAsync());

        public async Task<IActionResult> SetAproove(int id)
            => await this.aproovedService.SetAprooveAsync(id)
            .ContinueWith(_ => Redirect("/"));
    }
}
