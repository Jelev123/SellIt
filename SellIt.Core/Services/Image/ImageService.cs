namespace SellIt.Core.Services.Image
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.Constants.Error;
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

        public ImageService(IWebHostEnvironment environment,
            IRepository<Image> imageRepository)
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
                    var gallery = await CreateGalleryModelAsync(file, fileFolder);
                    fileDTO.Gallery.Add(gallery);
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                          ErrorMessages.DataDoesNotExist,
                          typeof(Image).Name));
            }
        }

        public async Task DeleteImageAsync(string imageId)
        {
            var image = imageRepository
                .All()
                .FirstOrDefault(x => x.ImageId == imageId)
                ?? throw new NullReferenceException(string.Format(
                             ErrorMessages.DataDoesNotExist,
                             typeof(Image).Name, "id", imageId));

            string filePath = Path.Combine(environment.WebRootPath, image.URL.TrimStart('/'));

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                          ErrorMessages.DataDoesNotExist,
                          typeof(Image).Name));
            }

            imageRepository.Delete(image);
            await imageRepository.SaveChangesAsync();
        }

        public async Task<string> UploadImageAsync(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(environment.WebRootPath, folderPath);

            using (var fileStream = new FileStream(serverFolder, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/" + folderPath;
        }

        private async Task<GalleryModel> CreateGalleryModelAsync(IFormFile file, string folderPath)
        {
            var imageUrl = await UploadImageAsync(folderPath, file);

            return new GalleryModel
            {
                Name = file.FileName,
                URL = imageUrl
            };
        }
    }
}
