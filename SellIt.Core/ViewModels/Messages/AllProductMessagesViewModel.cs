﻿namespace SellIt.Core.ViewModels.Messages
{
    using SellIt.Core.ViewModels.ReplyMessages;

    public class AllProductMessagesViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string ReplyText { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public DateTime Date { get; set; }

        public string Image { get; set; }

        public List<AllReplyProductMessagesViewModel> ReplyMessages { get; set; }
    }
}