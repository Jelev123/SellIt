namespace SellIt.Core.ViewModels.Home
{
    using SellIt.Core.ViewModels.Product;

    public class HomeViewModel
    {
        public int ProductForAprooveCount { get; set; }

        public int AllProducts { get; set; }

        public IEnumerable<IndexRandomViewModel> RandomProducts { get; set; }
    }
}
