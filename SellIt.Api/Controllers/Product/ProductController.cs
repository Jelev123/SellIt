namespace SellIt.Api.Controllers.Product
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Product;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
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
                : BadRequest(ProductConstants.ProductNotFound);
        }


        [Route("Delete")]
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await this.productService.GetByIdAsync(id);

            return  product != null 
                ? Ok(ProductConstants.DeletedProduct) 
                : BadRequest(ProductConstants.ProductNotFound);
        }
    }
}
