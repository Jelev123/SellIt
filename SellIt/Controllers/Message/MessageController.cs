namespace SellIt.Controllers.Message
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Areas.ViewModels;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Infrastructure.Data.Models;

    public class MessageController : Controller
    {
        private readonly IMessagesService messagesService;
        private readonly UserManager<User> userManager;


        public MessageController(IMessagesService messagesService, UserManager<User> userManager)
        {
            this.messagesService = messagesService;
            this.userManager = userManager;
        }

        public IActionResult AllProductMessages(int id)
        {
            var all = this.messagesService.AllProductMessages(id);
            return View(all);

        }
        public IActionResult SendMessage()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessageViewModel sendMessage, int id)
        {
            var userId = this.userManager.GetUserId(User);
            var userName = this.userManager.GetUserName(User);
            var send = this.messagesService.SendMessage(sendMessage, userId, userName, id);
            return this.Redirect("/");
        }

        public IActionResult ReplyMessage()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult ReplyMessage(SendMessageViewModel sendMessage, int id)
        {
            var userId = this.userManager.GetUserId(User);
            var userName = this.userManager.GetUserName(User);
            var reply = this.messagesService.ReplyMessage(sendMessage, userId, userName, id);
            return this.Redirect("/");
        }
    }
}
