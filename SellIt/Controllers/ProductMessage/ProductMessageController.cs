namespace SellIt.Controllers.ProductMessage
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.ProductMessage;
    using SellIt.Core.ViewModels.ProductMessage;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;

    public class ProductMessageController : Controller
    {
        private readonly IProductMessageService productMessageService;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext data;

        public ProductMessageController(IProductMessageService productMessageService, UserManager<User> userManager, ApplicationDbContext data)
        {
            this.productMessageService = productMessageService;
            this.userManager = userManager;
            this.data = data;
        }

        public IActionResult SendMessage(int id)
        {

            var userId = this.userManager.GetUserId(User);
            var product = this.data.Products.Where(s => s.Id == id).FirstOrDefault();
            var productMessages = this.data.ProductMessages.FirstOrDefault(s => s.ProductId == product.Id);

            if (productMessages == null || productMessages.UserId != userId)
            {
                return this.View();

            }
            else
            {
                return RedirectToAction("ReplyProductMessage", "ReplyProductMessage", new { productMessageId = productMessages.Id });
            }
           
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessageViewModel sendMessage, int id)
        {
            var userId = this.userManager.GetUserId(User);     
            this.productMessageService.SendMessage(sendMessage, userId, id);
            return this.Redirect("/");
        }

        public IActionResult AllMessages(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var allMessages = this.productMessageService.AllMessages(id, userId);
            return this.View(allMessages);
        }
    }
}
