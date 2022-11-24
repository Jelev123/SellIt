namespace SellIt.Controllers.ReplyProductMessage
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.ReplyProductMessage;
    using SellIt.Core.ViewModels.ReplayProductMessage;
    using SellIt.Infrastructure.Data.Models;

    public class ReplyProductMessageController : Controller
    {
        private readonly IReplyProductMessageService replyProductMessageService;
        private readonly UserManager<User> userManager;


        public ReplyProductMessageController(IReplyProductMessageService replyProductMessageService, UserManager<User> userManager)
        {
            this.replyProductMessageService = replyProductMessageService;
            this.userManager = userManager;
        }

        public IActionResult ReplyProductMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReplyProductMessage(ReplyProductMessagesViewModel replyProductMessage)
        {
            var userId = this.userManager.GetUserId(User);
            this.replyProductMessageService.ReplyMessage(replyProductMessage, userId);
            return this.RedirectToAction("MyProducts", "Product");
        }

        public IActionResult AllReplyedMessages(int id)
        {
            var allMessages = this.replyProductMessageService.AllReplyedMessages(id);
            return this.View(allMessages);
        }
    }
}
