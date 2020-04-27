using EssayCompetition.Data.Models;
using EssayCompetition.Data.Repositories;
using EssayCompetition.Services.Data.TeacherServices;
using EssayCompetition.Services.Data.Tests.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using EssayCompetition.Web.ViewModels.Teacher.Reviews;
using EssayCompetition.Services.Mapping;
using System.Reflection;

namespace EssayCompetition.Services.Data.Tests
{
    public class TeacherServiceTests
    {
        private Seeder seeder;

        public TeacherServiceTests()
        {
            this.seeder = new Seeder();
            AutoMapperConfig.RegisterMappings(
                typeof(Essay).GetTypeInfo().Assembly,
                typeof(EssayViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(
                typeof(Category).GetTypeInfo().Assembly,
                typeof(CategoryDropDownViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task GetEssayInfoTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var gradeRepository = new EfRepository<Grade>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var service = new TeacherService(essayRepository, categoryRepository, gradeRepository, essayTeacherRepository);

            var result = service.HasEssayWithId(essay.Id);

            Assert.True(result, "GetEssayInfo method does not work correctly");
        }

        [Fact]
        public async Task GradeEssayAsyncTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var gradeRepository = new EfRepository<Grade>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var service = new TeacherService(essayRepository, categoryRepository, gradeRepository, essayTeacherRepository);

            await service.GradeEssayAsync("stava", 12, essay.Id);

            Assert.True(context.Grades.First(x => x.EssayId == essay.Id).Points == 12, "GradeEssayAsync method does not work correctly");
        }

        [Fact]
        public async Task GetTeacherNotReviewedEssaysInRangeTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay1 = await this.seeder.SeedEssayAsync(context);
            var essay2 = await this.seeder.SeedEssayAsync(context);
            var user1 = await this.seeder.SeedUserAsync(context, "user1@abv.bg");
            var essayTeacher1 = await this.seeder.SeedEssayTeacher(context, user1.Id, essay1.Id);
            var essayTeacher2 = await this.seeder.SeedEssayTeacher(context, user1.Id, essay2.Id);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var gradeRepository = new EfRepository<Grade>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var service = new TeacherService(essayRepository, categoryRepository, gradeRepository, essayTeacherRepository);

            var result = service.GetTeacherNotReviewedEssaysInRange<EssayViewModel>(user1.Id, 1, 2);

            Assert.True(result.Count() == 2, "GetTeacherNotReviewedEssaysInRange method does not work correctly");
        }

        [Fact]
        public async Task GetTeacherNotReviewedEssaysCountTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay1 = await this.seeder.SeedEssayAsync(context);
            var essay2 = await this.seeder.SeedEssayAsync(context);
            var user1 = await this.seeder.SeedUserAsync(context, "user1@abv.bg");
            var essayTeacher1 = await this.seeder.SeedEssayTeacher(context, user1.Id, essay1.Id);
            var essayTeacher2 = await this.seeder.SeedEssayTeacher(context, user1.Id, essay2.Id);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var gradeRepository = new EfRepository<Grade>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var service = new TeacherService(essayRepository, categoryRepository, gradeRepository, essayTeacherRepository);

            var result = service.GetTeacherNotReviewedEssaysCount(user1.Id);

            Assert.True(result == context.Essays.Where(x => x.Graded == false).Count(), "GetTeacherNotReviewedEssaysCount method does not work correctly");
        }

        [Fact]
        public async Task GetAllAvilableCategoriesTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.seeder.SeedCategoryAsync(context, "cat1");
            await this.seeder.SeedCategoryAsync(context, "cat2");
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var gradeRepository = new EfRepository<Grade>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var service = new TeacherService(essayRepository, categoryRepository, gradeRepository, essayTeacherRepository);

            var result = service.GetAllAvilableCategories<CategoryDropDownViewModel>();

            Assert.True(result.Count() == context.Categories.Count(), "GetAllAvilableCategories method does not work correctly");
        }
    }
}
