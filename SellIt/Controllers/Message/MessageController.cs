namespace SellIt.Controllers.Message
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Infrastructure.Data.Models;
    using System.Security.Claims;

    public class MessageController : Controller
    {
        private readonly IMessagesService messagesService;
        private readonly UserManager<User> userManager;

        public MessageController(IMessagesService messagesService, UserManager<User> userManager)
        {
            this.messagesService = messagesService;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult SendMessage(int id, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var userName = this.User.Identity.Name;
                this.messagesService.SendMessage(userName, id, message);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Empty message" });
        }

        [HttpPost]
        public IActionResult ReplyMessage(string replyMessage, int id)
        {
            if (!string.IsNullOrEmpty(replyMessage))
            {
                var userName = this.User.Identity.Name;
                this.messagesService.ReplyMessage(replyMessage, userName, id);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Empty reply message" });
        }

        public IActionResult AllProductMessages(int id) => View(this.messagesService.AllProductMessages(id));

        public IActionResult AllMessages() => this.View(this.messagesService.AllMessages());

        public IActionResult GetProductMessageById(int id) => View(messagesService.GetProductMessageById(id));
    }
}
