namespace SellIt.Test.Controller
{
    using SellIt.Core.Services.Product;
    using SellIt.Infrastructure.Data.Models;
    using SellIt.Test.Mock;
    using Xunit;

    public class ProductServiceTest
    {
        private const int ProductId = 1;
        private const int InvalidProductId = 100;

        [Fact]
        public async Task ShouldReturnProductById()
        {
            var productService = await this.GetProductService();
            var result = await productService.GetByIdAsync(ProductId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldReturnAllProducts()
        {
            var productService = await this.GetProductService();
            var result = await productService.GetAllProductsAsync();
            Assert.NotNull(result);
        }

        private async Task<ProductService> GetProductService()
        {
            try
            {
                var data = DbMock.Instance;
                var userService = UserServiceMock.Instance;
                var products = new List<Product>();

                var product = new Product()
                {
                    ProductId = ProductId,
                    Name = "Test Product1",
                    CreatedUserId = "Test User1",
                    CategoryId = 1,
                    Description = "Description1",
                    PhoneNumber = "0894",
                    ProductAdress = "STZ"
                };

                var product2 = new Product()
                {
                    ProductId = 2,
                    Name = "Test Product2",
                    CreatedUserId = "Test User2",
                    CategoryId = 2,
                    Description = "Description2",
                    PhoneNumber = "0895",
                    ProductAdress = "RDV"
                };

                products.AddRange(new[] { product, product2 });
                await data.Products.AddRangeAsync(products);
                await data.SaveChangesAsync();
                return new ProductService(data, null, null, userService);
            }
            catch (Exception)
            {
                throw;
            }          
        }
    }
}
