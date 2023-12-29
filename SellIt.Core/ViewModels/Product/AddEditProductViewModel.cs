namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    public class AddEditProductViewModel
    {
        public int Id { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "Please enter a title")]
        [MaxLength(50)]
        [MinLength(3, ErrorMessage = "The title must be min {1} characters long.")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [MaxLength(300)]
        [MinLength(5, ErrorMessage = "The description must be min {1} characters long.")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Category")]
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        [DisplayName("Image")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }


        [DisplayName("Price")]
        [Required(ErrorMessage = "Please enter a price")]
        public decimal Price { get; set; }

        public string PhoneNumber { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Please enter an address")]
        public string Address { get; set; }
    }
}
