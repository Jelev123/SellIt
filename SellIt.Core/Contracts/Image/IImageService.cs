namespace SellIt.Core.Contracts.Image
{
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.ViewModels;

    public interface IImageService
    {
        Task DeleteImageAsync(string imageId);

        Task CheckGalleryAsync(GalleryFileDTO fileDTO);

        Task<string> UploadImageAsync(string folderPath, IFormFile file);
    }
}
