namespace SellIt.Api.Controllers.Product
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.Services.Image;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IImageService imageService;
        public ProductController(IProductService productService, IImageService imageService)
        {
            this.productService = productService;
            this.imageService = imageService;
        }


        [Route("AllProducts")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                return Ok(await this.productService.GetAllProductsAsync());

            }
            catch (Exception ex)
            {

                return BadRequest($"Serialization error: {ex.Message}");
            }
        }


        [Route("ById")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProductById(int id)
        {
            return await this.productService.GetByIdAsync(id) != null
                ? Ok(await this.productService.GetByIdAsync(id))
                : NotFound();
        }


        [Route("Delete")]
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await this.productService.GetByIdAsync(id);
            if (product != null)
            {
                return Ok("The product is deleted successfully");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
