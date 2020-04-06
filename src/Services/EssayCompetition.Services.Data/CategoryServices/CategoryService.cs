namespace EssayCompetition.Services.Data.CategoryServices
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> deletableEntityRepository;
        private readonly Cloudinary cloudinary;

        public CategoryService(IDeletableEntityRepository<Category> deletableEntityRepository, Cloudinary cloudinary)
        {
            this.deletableEntityRepository = deletableEntityRepository;
            this.cloudinary = cloudinary;
        }

        public async Task CreateAsync(string title, string description, string imageUrl)
        {
            var category = new Category()
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
            };

            await this.deletableEntityRepository.AddAsync(category);
            await this.deletableEntityRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var targetedCategory = this.deletableEntityRepository.All().First(category => category.Id == id);
            this.deletableEntityRepository.Delete(targetedCategory);
            await this.deletableEntityRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.deletableEntityRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAll<T>(int currentPage, int pageSize)
        {
            return this.deletableEntityRepository.All().Skip((currentPage - 1) * pageSize).Take(pageSize)
                .To<T>().ToList();
        }

        public int GetCount()
        {
            return this.deletableEntityRepository.All().Count();
        }

        public T GetWithId<T>(int id)
        {
            return this.deletableEntityRepository.All().Where(category => category.Id == id).AsQueryable().To<T>().First();
        }

        public async Task<bool> HasWithIdAsync(int id)
        {
            var result = await this.deletableEntityRepository.All().AnyAsync(category => category.Id == id);
            return result;
        }

        public async Task UpdateAsync(int id, string title, string description, string imageUrl)
        {
            this.deletableEntityRepository.Update(new Category { Id = id, Title = title, Description = description, ImageUrl = imageUrl });
            await this.deletableEntityRepository.SaveChangesAsync();
        }

        public async Task<string> UploadImageToCloudinaryAsync(IFormFile content)
        {
            var res = new ImageUploadResult();
            var file = content;
            byte[] destinationImage;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, destinationStream),
                };

                res = await this.cloudinary.UploadAsync(uploadParams);
            }

            return res.Uri.AbsoluteUri;
        }
    }
}
