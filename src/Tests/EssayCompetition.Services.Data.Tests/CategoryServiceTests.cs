namespace EssayCompetition.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using EssayCompetition.Data;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Data.Repositories;
    using EssayCompetition.Services.Data.CategoryServices;
    using EssayCompetition.Services.Data.ImageServices;
    using EssayCompetition.Services.Data.Tests.Common;
    using EssayCompetition.Services.Mapping;
    using EssayCompetition.Web.ViewModels.Administration.Category;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Xunit;

    public class CategoryServiceTests
    {
        public CategoryServiceTests()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(Category).GetTypeInfo().Assembly,
                typeof(CategoryViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CountTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var repository = new EfDeletableEntityRepository<Category>(context);
            var service = new CategoryService(repository);

            var countBefore = service.GetCount();
            await service.CreateAsync("test", "test", "#");
            var countAfter = service.GetCount();

            Assert.True(countAfter == countBefore + 1, "Count method does not work correctly");
        }

        [Fact]
        public async Task AddCategory()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var repository = new EfDeletableEntityRepository<Category>(context);
            var service = new CategoryService(repository);

            var initialCount = service.GetCount();
            await service.CreateAsync("test", "test", "#");
            var count = service.GetCount();

            Assert.True(count == initialCount + 1, "Create method does not work correctly");
        }

        [Fact]
        public async Task Delete()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var repository = new EfDeletableEntityRepository<Category>(context);
            var service = new CategoryService(repository);

            var initialCount = service.GetCount();
            var deleteId = service.GetFirstOrDefaultCategoryId();
            await service.DeleteAsync(deleteId);
            var count = service.GetCount();

            Assert.True(count == initialCount - 1, "Delete method does not work correctly");
        }

        [Fact]
        public async Task GetAll()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var repository = new EfDeletableEntityRepository<Category>(context);
            var service = new CategoryService(repository);

            var countBefore = service.GetCount();
            var allCategories = service.GetAll<CategoryViewModel>();
            var countAfter = allCategories.Count();

            Assert.True(countBefore == countAfter , "GetAll method does not work correctly");
        }

        [Fact]
        public async Task GetAllInRange()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var repository = new EfDeletableEntityRepository<Category>(context);
            var service = new CategoryService(repository);

            var count = service.GetCount();
            var allCategories = service.GetAll<CategoryViewModel>(1, count - 1);
            var countGetCategories = allCategories.Count();

            Assert.True(countGetCategories == count - 1, "GetAll method does not work correctly");
        }

        [Fact]
        public async Task GetWithId()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var repository = new EfDeletableEntityRepository<Category>(context);
            var service = new CategoryService(repository);

            var firstId = service.GetFirstOrDefaultCategoryId();
            var currentCategory = service.GetWithId<CategoryViewModel>(firstId);

            Assert.True(firstId == currentCategory.Id, "GetWithId method does not work correctly");
        }

        [Fact]
        public async Task HasWithId()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var repository = new EfDeletableEntityRepository<Category>(context);
            var service = new CategoryService(repository);

            var firstId = service.GetFirstOrDefaultCategoryId();
            var hasWithId = await service.HasWithIdAsync(firstId);

            Assert.True(hasWithId == true, "GetWithId method does not work correctly");
        }

        [Fact]
        public async Task Update()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var repository = new EfDeletableEntityRepository<Category>(context);
            var service = new CategoryService(repository);

            var firstId = service.GetFirstOrDefaultCategoryId();
            var current = service.GetWithId<CategoryViewModel>(firstId);
            await service.UpdateAsync(1, current.Title,current.Description ,current.ImageUrl + "*" );
            var currentAfterUpdate = service.GetWithId<CategoryViewModel>(firstId);

            Assert.True(currentAfterUpdate.ImageUrl == current.ImageUrl + "*", "Update method does not work correctly");
        }

        private async Task SeedData(ApplicationDbContext context)
        {

            var category = new Category()
            {
                Title = "Basic",
                Description = "No rules",
                ImageUrl = "#",
            };
            var category2 = new Category()
            {
                Title = "Basic",
                Description = "No rules",
                ImageUrl = "#",
            };
            var category3 = new Category()
            {
                Title = "Basic",
                Description = "No rules",
                ImageUrl = "#",
            };

            context.Categories.Add(category);
            context.Categories.Add(category2);
            context.Categories.Add(category3);
            await context.SaveChangesAsync();
        }
    }
}
