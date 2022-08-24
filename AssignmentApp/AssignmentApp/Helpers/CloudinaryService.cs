using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Xamarin.Essentials;

namespace AssignmentApp.Helpers
{
    public static class CloudinaryService
    {
        static CloudinaryDotNet.Cloudinary cloudinary;
        static CloudinaryService()
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("gigacloud", "369833398774284", "AvPQ9viChEg1qRxjdWy9OGlmNNQ");
            cloudinary = new CloudinaryDotNet.Cloudinary(account);
        }

        public static async Task<(string, string)> Upload(FileResult file)
        {
            var x = new ImageUploadParams
            {
               File = new CloudinaryDotNet.FileDescription(file.FileName, await file.OpenReadAsync())
            };

            var uploadResult = await cloudinary.UploadAsync(x);
            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                return (uploadResult.Url.ToString(), uploadResult.PublicId);

            return (null, null);
        }
    }
}

