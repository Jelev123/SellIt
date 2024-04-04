namespace SellIt.Controllers.Image
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Image;

    public class ImageController : Controller
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public async Task<IActionResult> DeleteImage(string id, int productId)
        {
            return await imageService.DeleteImageAsync(id)
                             .ContinueWith(_ => RedirectToAction("EditProduct", "Product", 
                             new { id = productId }));
        }
    }
}
