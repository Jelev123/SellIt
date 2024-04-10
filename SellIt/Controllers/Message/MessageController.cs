namespace SellIt.Controllers.Message
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Admin;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Core.Contracts.User;

    public class MessageController : Controller
    {
        private readonly IMessagesService messagesService;
        private readonly IAdminService adminService;
        private readonly string UserName;

        public MessageController(IMessagesService messagesService, IAdminService adminService)
        {
            this.messagesService = messagesService;
            UserName = adminService.CurrentUserName();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMessage(int id, string message)
        {
            return !string.IsNullOrEmpty(message)
             ? await messagesService.SendMessageAsync(UserName, id, message)
             .ContinueWith(_ => Json(new { success = true }))
             : await Task.FromResult(Json(new { success = false, message = MessageConstants.EmptyMessage }));
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ReplyMessage(string replyMessage, int id)
        {
            return !string.IsNullOrEmpty(replyMessage)
               ? await messagesService.ReplyMessageAsync(UserName, replyMessage, id)
               .ContinueWith(_ => Json(new { success = true }))
               : await Task.FromResult(Json(new { success = false, message = MessageConstants.EmptyMessage }));
        }

        [Authorize]
        public async Task<IActionResult> AllProductMessages(int id)
        {
            return View(await messagesService.AllProductMessagesAsync(id));
        }

        [Authorize]
        public async Task<IActionResult> AllMessages()
        {
            return View(await messagesService.AllMessagesAsync());
        }

        [Authorize]
        public async Task<IActionResult> GetProductMessageById(int id)
        {
            return View(await messagesService.GetProductMessageByIdAsync(id));
        }
    }
}
