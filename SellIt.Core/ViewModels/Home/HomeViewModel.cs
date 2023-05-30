namespace SellIt.Core.ViewModels.Home
{
    using SellIt.Core.ViewModels.Category;
    using SellIt.Core.ViewModels.Product;

    public class HomeViewModel
    {
        public int Id { get; set; }

        public int ProductForAprooveCount { get; set; }

        public int AllProducts { get; set; }

        public int ProductMessages { get; set; }

        public int LikedCount { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<IndexRandomViewModel> RandomProducts { get; set; }

        public IEnumerable<AllCategoriesViewModel> AllCategories { get; set; }

    }
}
