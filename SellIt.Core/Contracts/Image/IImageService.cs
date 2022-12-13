namespace SellIt.Core.Contracts.Image
{
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.ViewModels.Product;

    public interface IImageService
    {
        void DeleteImage(string imageId);

        Task CheckGallery(AddEditProductViewModel model);

        Task<string> UploadImage(string folderPath, IFormFile file);
    }
}
