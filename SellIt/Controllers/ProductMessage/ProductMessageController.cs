namespace SellIt.Controllers.ProductMessage
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.ProductMessage;
    using SellIt.Core.ViewModels.ProductMessage;
    using SellIt.Infrastructure.Data.Models;

    public class ProductMessageController : Controller
    {
        private readonly IProductMessageService productMessageService;
        private readonly UserManager<User> userManager;


        public ProductMessageController(IProductMessageService productMessageService, UserManager<User> userManager)
        {
            this.productMessageService = productMessageService;
            this.userManager = userManager;
        }

        public IActionResult SendMessage()
        {

            return this.View();
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessageViewModel sendMessage, int id)
        {
            var userId = this.userManager.GetUserId(User);

            var messages = this.productMessageService.SendMessage(sendMessage, userId, id);

            return this.Redirect("/");
        }

        public IActionResult AllMessages(int id)
        {
            var allMessages = this.productMessageService.AllMessages(id);
            return this.View(allMessages);
        }
    }
}
