namespace SellIt.Api.Controllers.Product
{
    using Microsoft.AspNetCore.Mvc;
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
                return Ok(this.productService.GetAllProducts());

            }
            catch (ArgumentException ae)
            {

                return BadRequest(ae.Message);
            }
        }
    }
}
