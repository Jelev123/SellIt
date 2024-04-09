namespace SellIt.Api.Controllers.Product
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels;
    using SellIt.Core.ViewModels.Product;


    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }


        [Route("addProduct")]
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] AddEditProductViewModel model, [FromForm] GalleryFileDTO fileDto)
        {
            await productService.AddProductAsync(model, fileDto);

            return Ok(model);
        }

        [Route("allProducts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllProductViewModel>>> GetAllProducts()
        {
            return Ok(await productService.GetAllProductsAsync());
        }


        [Route("byId")]
        [HttpGet]
        public async Task<ActionResult<GetByIdAndLikeViewModel>> GetProductById(int id)
        {
            return Ok(await productService.GetByIdAsync(id));
        }


        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await productService.DeleteProductAsync(id);

            return Ok();
        }

        [Route("edit")]
        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] AddEditProductViewModel editProduct, int id, [FromForm] GalleryFileDTO fileDTO)
        {
            await productService.EditProductAsync(editProduct, id, fileDTO);

            return Ok();
        }
    }
}
