namespace SellIt.Core.ViewModels.Category
{
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.Constants.Attributes;
    using System.ComponentModel.DataAnnotations;

    public class CreateCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ValidateMessages.Required)]
        [StringLength(Attributes.CategoryNameMaxLenght,
            ErrorMessage = ValidateMessages.MinMaxLength,
            MinimumLength = Attributes.CategoryNameMinLenght)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidateMessages.Required)]
        public IFormFile Image { get; set; }
    }
}
