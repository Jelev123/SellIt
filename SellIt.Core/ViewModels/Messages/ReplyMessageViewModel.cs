namespace SellIt.Core.ViewModels.Messages
{
    using SellIt.Core.Constants.Attributes;
    using SellIt.Core.Handlers.Error.Class;
    using System.ComponentModel.DataAnnotations;

    public class ReplyMessageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidateMessages.Required)]
        [StringLength(Attributes.MessageMaxLenght,
           ErrorMessage = ValidateMessages.MinMaxLength,
           MinimumLength = Attributes.MessageMinLenght)]
        public string ReplyText { get; set; }

        public string ReplyerUserName { get; set; }

        public DateTime ReplyerDate { get; set; } = DateTime.UtcNow;
    }
}
