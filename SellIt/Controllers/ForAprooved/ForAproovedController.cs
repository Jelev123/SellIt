namespace SellIt.Controllers.ForAprooved
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.ForAprooved;

    public class ForAproovedController : Controller
    {
        private readonly IForAproovedService aproovedService;

        public ForAproovedController(IForAproovedService aproovedService)
        {
            this.aproovedService = aproovedService;
        }

        [Authorize(Roles = UserConstants.Role.Administrator)]
        public async Task<IActionResult> GetAllProductsForAproove()
        {
            return View(await aproovedService.GetAllProductsForAprooveAsync());
        }

        [Authorize(Roles = UserConstants.Role.Administrator)]
        public async Task<IActionResult> SetAproove(int id)
        {
            return await aproovedService.SetAprooveAsync(id)
             .ContinueWith(_ => Redirect("/"));
        }
    }
}
