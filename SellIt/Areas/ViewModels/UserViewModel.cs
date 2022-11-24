namespace SellIt.Areas.ViewModels
{
    using SellIt.Core.ViewModels.ProductMessage;
    using SellIt.Core.ViewModels.ReplayProductMessage;

    public class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public List<ReplyProductMessagesViewModel> ReplayedProductMessages { get; set; }
    }
}
