namespace SellIt.Controllers.Message
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Core.Contracts.User;

    public class MessageController : Controller
    {
        private readonly IMessagesService messagesService;
        private readonly IUserService userService;
        private readonly string UserName;

        public MessageController(IMessagesService messagesService, IUserService userService)
        {
            this.messagesService = messagesService;
            UserName = userService.CurrentUserName();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int id, string message)
              => !string.IsNullOrEmpty(message)
              ? await messagesService.SendMessageAsync(UserName, id, message)
              .ContinueWith(_ => Json(new { success = true }))
              : await Task.FromResult(Json(new { success = false, message = MessageConstants.EmptyMessage }));

        [HttpPost]
        public async Task<IActionResult> ReplyMessage(string replyMessage, int id)
        {
            return !string.IsNullOrEmpty(replyMessage)
               ? await messagesService.ReplyMessageAsync(UserName, replyMessage, id)
               .ContinueWith(_ => Json(new { success = true }))
               : await Task.FromResult(Json(new { success = false, message = MessageConstants.EmptyMessage }));
        }

        public async Task<IActionResult> AllProductMessages(int id)
        {
            return View(await this.messagesService.AllProductMessagesAsync(id));
        }

        public async Task<IActionResult> AllMessages()
        {
            return this.View(await this.messagesService.AllMessagesAsync());
        }

        public async Task<IActionResult> GetProductMessageById(int id)
        {
            return View(await this.messagesService.GetProductMessageByIdAsync(id));
        }
    }
}
