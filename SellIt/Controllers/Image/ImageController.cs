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


        public IActionResult DeleteImage(string id, int productId)
        {
            this.imageService.DeleteImage(id);
            return this.RedirectToAction("EditProduct", "Product", new {id = productId });
        }
    }
}
