namespace SellIt.Core.Services.Cloudinary
{
    using Microsoft.AspNetCore.Http;
    using SellIt.Core.Contracts.Cloudinary;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    public class CloudinaryService : ICloduinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public async Task<string> UploadPictureAsync(List<IFormFile> pictureFile, string fileName)
        {
            //byte[] destinationData;

            //using (var ms = new MemoryStream())
            //{
            //    pictureFile.CopyToAsync(ms);
            //    destinationData = ms.ToArray();
            //}

            UploadResult uploadResult = null;


            foreach (var item in pictureFile)
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "SellIt",
                    File = new FileDescription(item.FileName)
                };

                uploadResult = this.cloudinaryUtility.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
