namespace SellIt.Controllers.Message
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Areas.ViewModels;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Core.ViewModels.Messages;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;

    public class MessageController : Controller
    {
        private readonly IMessagesService messagesService;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext data;


        public MessageController(IMessagesService messagesService, UserManager<User> userManager, ApplicationDbContext data)
        {
            this.messagesService = messagesService;
            this.userManager = userManager;
            this.data = data;
        }

        public IActionResult AllProductMessages(int id)
        {
            var all = this.messagesService.AllProductMessages(id);
            return View(all);

        }

        [HttpPost]
        public IActionResult SendMessage(int id, string message)
        {
            var userId = this.userManager.GetUserId(User);
            var userName = this.userManager.GetUserName(User);
            this.messagesService.SendMessage(userId, userName, id, message);
            return Json(new { success = true });

        }

        [HttpPost]
        public IActionResult ReplyMessage(string replyMessage, int id)
        {
            var userId = this.userManager.GetUserId(User);
            var userName = this.userManager.GetUserName(User);
            this.messagesService.ReplyMessage(replyMessage, userId, userName, id);
            return Json(new { success = true });
        }

        public IActionResult AllMessages()
        {
            var userId = this.userManager.GetUserId(User);
            var allMessages = this.messagesService.AllMessages(userId);
            return this.View(allMessages);
        }

        public IActionResult GetProductMessageById(int id)
        {
            var message = this.messagesService.GetProductMessageById(id);
            return this.View(message);
        }
    }
}
