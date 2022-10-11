namespace SellIt.Core.Contracts.Cloudinary
{
    using Microsoft.AspNetCore.Http;

    public interface ICloduinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);
    }
}
