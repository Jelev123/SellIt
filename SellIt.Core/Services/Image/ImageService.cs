namespace SellIt.Core.Services.Image
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.Repository;
    using SellIt.Core.ViewModels;
    using SellIt.Infrastructure.Data.Models;
    using System;
    using System.Threading.Tasks;

    public class ImageService : IImageService
    {
        private readonly IRepository<Image> imageRepository;
        private readonly IWebHostEnvironment environment;

        public ImageService(IWebHostEnvironment environment, IRepository<Image> imageRepository)
        {
            this.environment = environment;
            this.imageRepository = imageRepository;
        }

        public async Task CheckGalleryAsync(GalleryFileDTO fileDTO)
        {
            var fileFolder = Constants.ProductConstants.ProductsImagesFolder;

            if (fileDTO.GalleryFiles != null)
            {
                fileDTO.Gallery = new List<GalleryModel>();

                foreach (var file in fileDTO.GalleryFiles)
                {
                    var gallery = new GalleryModel()
                    {
                        Name = file.FileName,
                        URL = await UploadImageAsync(fileFolder, file)
                    };
                    fileDTO.Gallery.Add(gallery);
                }
            }
        }

        public async Task DeleteImageAsync(string imageId)
        {
            var image = this.imageRepository.All().FirstOrDefault(x => x.ImageId == imageId);

            string filePath = Path.Combine(environment.WebRootPath, image.URL.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            imageRepository.Delete(image);
            await imageRepository.SaveChangesAsync();
        }

        public async Task<string> UploadImageAsync(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(environment.WebRootPath, folderPath);

            file.CopyTo(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}
