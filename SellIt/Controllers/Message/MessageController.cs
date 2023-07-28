namespace SellIt.Controllers.Message
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Messages;

    public class MessageController : Controller
    {
        private readonly IMessagesService messagesService;

        public MessageController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int id, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var userName = this.User.Identity.Name;
                await this.messagesService.SendMessageAsync(userName, id, message);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Empty message" });
        }

        [HttpPost]
        public async Task<IActionResult> ReplyMessage(string replyMessage, int id)
        {
            if (!string.IsNullOrEmpty(replyMessage))
            {
                var userName = this.User.Identity.Name;
                await this.messagesService.ReplyMessageAsync(replyMessage, userName, id);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Empty reply message" });
        }

        public async Task<IActionResult> AllProductMessages(int id) => View(await this.messagesService.AllProductMessagesAsync(id));

        public async Task<IActionResult> AllMessages() => this.View(await this.messagesService.AllMessagesAsync());

        public async Task<IActionResult> GetProductMessageById(int id) => View(await this.messagesService.GetProductMessageByIdAsync(id));
    }
}
