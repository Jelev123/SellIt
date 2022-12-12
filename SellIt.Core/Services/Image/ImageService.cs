namespace SellIt.Core.Services.Image
{
    using SellIt.Core.Contracts.Image;
    using SellIt.Infrastructure.Data;

    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext data;

        public ImageService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void DeleteImage(string imageId)
        {
            var image = data.Images.FirstOrDefault(x => x.ImageId == imageId);
            data.Remove(image);
            data.SaveChanges();
        }
    }
}
