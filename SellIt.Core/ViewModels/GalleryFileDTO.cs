namespace SellIt.Core.ViewModels
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    public class GalleryFileDTO
    {
        public IFormFileCollection GalleryFiles { get; set; }
        [BindNever]
        [ValidateNever]
        public List<GalleryModel> Gallery { get; set; }
    }
}
