namespace SellIt.Api.Controllers.Product
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data.Models;
    using System.Security.Claims;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment environment;

        public ProductController(IProductService productService, UserManager<User> userManager, IWebHostEnvironment environment)
        {
            this.productService = productService;
            this.userManager = userManager;
            this.environment = environment;
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

        //[Route("AddProducts")]
        //[HttpPost]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //public IActionResult AddProducts(AddEditProductViewModel addProduct)
        //{

        //    try
        //    {
        //        var user = HttpContext.User.FindFirstValue("userId");
        //        return Ok(this.productService.AddProduct(addProduct, user);

        //    }
        //    catch (ArgumentException ae)
        //    {

        //        return BadRequest(ae.Message);
        //    }
        //}
    }
}
