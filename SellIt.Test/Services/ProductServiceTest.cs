namespace SellIt.Test.Controller
{
    using SellIt.Core.Services.Product;
    using SellIt.Infrastructure.Data.Models;
    using SellIt.Test.Mock;
    using Xunit;

    public class ProductServiceTest
    {
        //private const int ProductId = 1;
        //private const int InvalidProductId = 100;

        //[Fact]
        //public async Task ShouldReturnProductById()
        //{
        //    var productService = await this.GetProductService();
        //    var result = await productService.GetByIdAsync(ProductId);
        //    Assert.NotNull(result);
        //}

    //    private async Task<ProductService> GetProductService()
    //    {
    //        var data = DbMock.Instance;
    //        var userService = UserServiceMock.Instance;
    //        var products = new List<Product>();

    //        var product = new Product()
    //        {
    //            ProductId = 1,
    //            Name = "Test Product",
    //            CreatedUserId = "Test User",
    //            CategoryId = 1,
    //            Images = new List<Image>()
    //{
    //    new Image() { URL = "https://example.com/image1.jpg" },
    //    new Image() { URL = "https://example.com/image2.jpg" },
    //    new Image() { URL = "https://example.com/image3.jpg" }
    //},
    //            Category = new Category() { Id = 1, Name = "Test Category" },
    //            Description = "Test Description",
    //            PhoneNumber = "1234567890",
    //            ProductAdress = "Test Address",
    //            User = new User() { Id = "Test User", UserName = "Test User" }
    //        };

    //        var product2 = new Product()
    //        {
    //            ProductId =2,
    //            Name = "Test Product2",
    //            CreatedUserId = "Test User",
    //            CategoryId = 2,
    //            Images = new List<Image>()
    //{
    //    new Image() { URL = "https://example.com/image1.jpg" },
    //    new Image() { URL = "https://example.com/image2.jpg" },
    //    new Image() { URL = "https://example.com/image3.jpg" }
    //},
    //            Category = new Category() { Id = 2, Name = "Test Category2" },
    //            Description = "Test Description",
    //            PhoneNumber = "1234567890",
    //            ProductAdress = "Test Address",
    //            User = new User() { Id = "Test User2", UserName = "Test User2" }
    //        };

    //        products.AddRange(new[] { product, product2 });
    //        await data.Products.AddRangeAsync(products);
    //        await data.SaveChangesAsync();
    //        return new ProductService(data, null, null, userService);
    //    }
    }
}
