namespace SellIt.Core.ViewModels.Product
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    public class GetByIdAndLikeViewModel
    {
        public int Id { get; set; }

        public int Viewed { get; set; }

        public int LikedCount { get; set; }

        public bool IsLiked { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public bool IsAprooved { get; set; }

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
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        public decimal Price { get; set; }

        public string AddressId { get; set; }
    }
}
