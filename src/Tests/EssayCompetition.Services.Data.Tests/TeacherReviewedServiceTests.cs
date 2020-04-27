namespace EssayCompetition.Services.Data.Tests
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Data.Repositories;
    using EssayCompetition.Services.Data.TeacherReviewedServices;
    using EssayCompetition.Services.Data.Tests.Common;
    using EssayCompetition.Services.Mapping;
    using EssayCompetition.Web.ViewModels.Teacher.Reviewed;
    using Xunit;

    public class TeacherReviewedServiceTests
    {
        private Seeder seeder;

        public TeacherReviewedServiceTests()
        {
            this.seeder = new Seeder();
            AutoMapperConfig.RegisterMappings(
                typeof(Essay).GetTypeInfo().Assembly,
                typeof(EssayViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(
                typeof(Grade).GetTypeInfo().Assembly,
                typeof(GradeViewModel).GetTypeInfo().Assembly);
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
            var service = new TeacherReviewedService(essayRepository, categoryRepository, gradeRepository, essayTeacherRepository);

            var essayInfo = service.GetEssayInfo<EssayViewModel>(essay.Id);

            Assert.True(essay.Id == essayInfo.Id, "GetEssayInfo method does not work correctly");
        }

        [Fact]
        public async Task HasEssayWithIdTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var gradeRepository = new EfRepository<Grade>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var service = new TeacherReviewedService(essayRepository, categoryRepository, gradeRepository, essayTeacherRepository);

            var result = service.HasEssayWithId(essay.Id);

            Assert.True(result == true, "HasEssayWithId method does not work correctly");
        }

        [Fact]
        public async Task GetGradeViewModelTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var grade = await this.seeder.AddGradeAsync(context, essay.Id);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var gradeRepository = new EfRepository<Grade>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var service = new TeacherReviewedService(essayRepository, categoryRepository, gradeRepository, essayTeacherRepository);

            var result = service.GetGradeViewModel<GradeViewModel>(essay.Id);

            Assert.True(grade.Points == result.Points, "GetGradeViewModel method does not work correctly");
        }

        [Fact]
        public async Task GetAllReviewedEssayFromTecherInRangeTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay1 = await this.seeder.SeedGradedEssayAsync(context);
            var essay2 = await this.seeder.SeedGradedEssayAsync(context);
            await this.seeder.AddGradeAsync(context, essay1.Id);
            await this.seeder.AddGradeAsync(context, essay2.Id);
            var user1 = await this.seeder.SeedUserAsync(context, "user1@abv.bg");
            var essayTeacher1 = await this.seeder.SeedEssayTeacher(context, user1.Id, essay1.Id);
            var essayTeacher2 = await this.seeder.SeedEssayTeacher(context, user1.Id, essay2.Id);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var gradeRepository = new EfRepository<Grade>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var service = new TeacherReviewedService(essayRepository, categoryRepository, gradeRepository, essayTeacherRepository);

            var result = service.GetAllReviewedEssayFromTecherInRange<EssayViewModel>(user1.Id, 1, 2);

            Assert.True(result.Count() == 2, "GetAllReviewedEssayFromTecherInRange method does not work correctly");
        }

        [Fact]
        public async Task GetAllReviewedEssayFromTecherCountTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay1 = await this.seeder.SeedGradedEssayAsync(context);
            var essay2 = await this.seeder.SeedGradedEssayAsync(context);
            await this.seeder.AddGradeAsync(context, essay1.Id);
            await this.seeder.AddGradeAsync(context, essay2.Id);
            var user1 = await this.seeder.SeedUserAsync(context, "user1@abv.bg");
            var essayTeacher1 = await this.seeder.SeedEssayTeacher(context, user1.Id, essay1.Id);
            var essayTeacher2 = await this.seeder.SeedEssayTeacher(context, user1.Id, essay2.Id);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var gradeRepository = new EfRepository<Grade>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var service = new TeacherReviewedService(essayRepository, categoryRepository, gradeRepository, essayTeacherRepository);

            var result = service.GetAllReviewedEssayFromTecherCount(user1.Id);

            Assert.True(result == 2, "GetAllReviewedEssayFromTecherCount method does not work correctly");
        }
    }
}
