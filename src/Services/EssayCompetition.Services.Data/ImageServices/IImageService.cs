namespace EssayCompetition.Services.Data.ImageServices
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        Task<string> UploadImageToCloudinaryAsync(IFormFile content);
    }
}
