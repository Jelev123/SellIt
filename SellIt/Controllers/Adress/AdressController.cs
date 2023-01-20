namespace SellIt.Controllers.Adress
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Adress;
    using SellIt.Infrastructure.Data.Models;

    public class AdressController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly IAdressService adressService;

        public AdressController(UserManager<User> userManager, IAdressService adressService)
        {
            this.userManager = userManager;
            this.adressService = adressService;
        }

        public async Task<IActionResult> AddressByUserId()
        {
            var userId = this.userManager.GetUserId(User);
            this.adressService.AddressByUserId(userId);
            return this.View("/");
        }

    }
}

