namespace SellIt.Core.ViewModels.Product
{
    public class AllProductsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Viewed { get; set; }

        public int Liked { get; set; }

        public int ClickCounter { get; set; }

        public string? CategoryName { get; set; }

        public string Image { get; set; }

        public string UserId { get; set; }

        public bool IsAprooved { get; set; }
    }
}
