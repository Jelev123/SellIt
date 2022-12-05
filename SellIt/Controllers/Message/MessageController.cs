namespace SellIt.Controllers.Message
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Areas.ViewModels;
    using SellIt.Core.Contracts.Messages;
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
        public IActionResult SendMessage(int id)
        {
            //var userId = this.userManager.GetUserId(User);
            //var message = this.data.Messages.Where(s => s.UserId == userId).FirstOrDefault();

            //if (message != null && message.UserId == userId)
            //{
            //    return RedirectToAction("ReplyMessage", new { id = message.Id });
            //}
            return this.View();
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessageViewModel sendMessage, int id)
        {
            var userId = this.userManager.GetUserId(User);
            var userName = this.userManager.GetUserName(User);
            this.messagesService.SendMessage(sendMessage, userId, userName, id);
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
        public IActionResult GetMessageByUserId(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var message = this.messagesService.GetMessageByUserId(userId, id);
            return this.View(message);
        }
    }
}
