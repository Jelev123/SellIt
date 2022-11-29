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

        public IActionResult AllProductMessages(int id)
        {
            var all = this.messagesService.AllProductMessages(id);
            return View(all);

        }
    }
}
