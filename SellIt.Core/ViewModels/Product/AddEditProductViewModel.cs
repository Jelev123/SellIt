namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using SellIt.Core.Constants.Attributes;

    public class AddEditProductViewModel
    {
        public int Id { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = ValidateMessages.Required)]
        [StringLength(Attributes.ProductNameMaxLength,
            ErrorMessage = ValidateMessages.MinMaxLength,
            MinimumLength = Attributes.ProductNameMinLength)]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = ValidateMessages.Required)]
        [StringLength(Attributes.DescriptionMaxLenght,
            ErrorMessage = ValidateMessages.MinMaxLength,
            MinimumLength = Attributes.DescriptionMinLenght)]
        public string Description { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = ValidateMessages.Required)]
        [StringLength(Attributes.CategoryNameMaxLenght,
            ErrorMessage = ValidateMessages.MinMaxLength,
            MinimumLength = Attributes.CategoryNameMinLenght)]
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        [DisplayName("Image")]
        [Required(ErrorMessage = ValidateMessages.Required)]
        public IFormFileCollection GalleryFiles { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = ValidateMessages.Required)]
        public decimal Price { get; set; }

        public string PhoneNumber { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = ValidateMessages.Required)]
        public string Address { get; set; }
    }
}
