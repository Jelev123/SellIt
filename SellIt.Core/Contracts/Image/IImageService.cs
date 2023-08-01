namespace SellIt.Core.Contracts.Image
{
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.ViewModels.Product;

    public interface IImageService
    {
        Task DeleteImageAsync(string imageId);

        Task CheckGalleryAsync(AddEditProductViewModel model);

        Task<string> UploadImageAsync(string folderPath, IFormFile file);
    }
}
