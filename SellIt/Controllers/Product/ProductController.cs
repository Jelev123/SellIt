namespace SellIt.Controllers.Product
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.Contracts.Search;
    using SellIt.Core.ViewModels;
    using SellIt.Core.ViewModels.Category;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data.Models;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly ISearchService searchService;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment environment;

        public ProductController(IProductService productService, ICategoryService categoryService, UserManager<User> userManager, IWebHostEnvironment environment, ISearchService searchService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userManager = userManager;
            this.environment = environment;
            this.searchService = searchService;
        }


        [Authorize]
        public IActionResult AddProduct()
        {
            var categories = this.categoryService.GetAllCategories<AllCategoriesViewModel>();

            this.ViewData["categories"] = categories.Select(s => new AddProductViewModel
            {
                CategoryName = s.Name,
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel addProduct)
        {
            var user = this.userManager.GetUserId(User);

            //if (!ModelState.IsValid)
            //{
            //    var categories = this.categoryService.GetAllCategories<AllCategoriesViewModel>();

            //    this.ViewData["categories"] = categories.Select(s => new AddProductViewModel
            //    {
            //        CategoryName = s.Name,
            //    }).ToList();
            //    return this.View(addProduct);
            //}

            if (addProduct.GalleryFiles != null)
            {
                string folder = "images/gallery/";

                addProduct.Gallery = new List<GalleryModel>();

                foreach (var file in addProduct.GalleryFiles)
                {
                    var gallery = new GalleryModel()
                    {
                        Name = file.FileName,
                        URL = await UploadImage(folder, file)
                    };
                    addProduct.Gallery.Add(gallery);
                }
            }
            this.productService.AddProduct(addProduct, user, $"{this.environment.WebRootPath}/images");
            return this.Redirect("/");
        }

        public IActionResult GetProductById(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var product = this.productService.GetById(id, userId);
            return this.View(product);
        }
        public IActionResult MyProducts()
        {
            var userId = this.userManager.GetUserId(User);
            var products = this.productService.MyProducts(userId);
            return this.View(products);
        }

        public IActionResult AllProducts()
        {
            var allProducts = this.productService.GetAllProducts();
            return this.View(allProducts);
        }
        public IActionResult Like(int id)
        {
            var currentUserId = this.userManager.GetUserId(User);
            var productToLike = this.productService.Like(id, currentUserId);
            return this.View(productToLike);
        }

        public IActionResult Search(string searchName)
        {
            
            this.ViewData["searchProduct"] = searchName;
            var searchedProduct = this.searchService.SearchProduct(searchName);

            if (searchedProduct == null)
            {
                return this.View(searchName);
            }
            return this.View(searchedProduct);
        }

        public IActionResult SearchCategory(string searchName)
        {

            this.ViewData["searchCategory"] = searchName;
            var searchedCategory = this.searchService.SearchProduct(searchName);

            if (searchedCategory == null)
            {
                return this.View(searchName);
            }
            return this.View(searchedCategory);
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(environment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

        public IActionResult SendMessage()
        {

            return this.View();
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessageViewModel sendMessage,int id)
        {
            var userId = this.userManager.GetUserId(User);

            var messages = this.productService.SendMessage(sendMessage, userId,id);

            return this.Redirect("/");
        }

        public IActionResult AllMessages(int id)
        {
           var allMessages =this.productService.AllMessages(id); 
            return this.View(allMessages);
        }
    }
}
