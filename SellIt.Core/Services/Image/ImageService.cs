namespace SellIt.Core.Services.Image
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.Constants;
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.ViewModels;
    using SellIt.Core.ViewModels.Product;
    using SellIt.Infrastructure.Data;
    using System;
    using System.Threading.Tasks;

    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext data;
        private readonly IWebHostEnvironment environment;

        public ImageService(ApplicationDbContext data, IWebHostEnvironment environment)
        {
            this.data = data;
            this.environment = environment;
        }

        public async Task CheckGallery(AddEditProductViewModel model)
        {
            var fileFolder = "images/gallery/";

            if (model.GalleryFiles != null)
            {
                model.Gallery = new List<GalleryModel>();

                foreach (var file in model.GalleryFiles)
                {
                    var gallery = new GalleryModel()
                    {
                        Name = file.FileName,
                        URL = await UploadImage(fileFolder, file)
                    };
                    model.Gallery.Add(gallery);
                }
            }        
        }

        public void DeleteImage(string imageId)
        {
            var image = data.Images.FirstOrDefault(x => x.ImageId == imageId);

            string filePath = Path.Combine(environment.WebRootPath, image.URL.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            data.Remove(image);
            data.SaveChanges();
        }

        public async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(environment.WebRootPath, folderPath);

            file.CopyTo(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }
    }
}
