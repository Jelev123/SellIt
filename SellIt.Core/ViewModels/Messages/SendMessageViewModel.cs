namespace SellIt.Areas.ViewModels
{
    using SellIt.Core.Constants.Attributes;
    using System.ComponentModel.DataAnnotations;

    public class SendMessageViewModel
    {
        [Required(ErrorMessage = ValidateMessages.Required)]
        [StringLength(Attributes.MessageMaxLenght,
           ErrorMessage = ValidateMessages.MinMaxLength,
           MinimumLength = Attributes.MessageMinLenght)]
        public string Text { get; set; }

    }
}
