namespace EssayCompetition.Services.Data.Tests
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Data.Repositories;
    using EssayCompetition.Services.Data.EssayServices;
    using EssayCompetition.Services.Data.Tests.Common;
    using EssayCompetition.Services.Mapping;
    using EssayCompetition.Web.ViewModels.Essays;
    using Xunit;

    public class EssayServiceTests
    {
        private const int CurrentPage = 1;
        private const int PageSize = 2;
        private Seeder seeder;

        public EssayServiceTests()
        {
            this.seeder = new Seeder();
            AutoMapperConfig.RegisterMappings(
                typeof(Essay).GetTypeInfo().Assembly,
                typeof(EssayViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task GetEssayDetailsTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var service = new EssayService(essayRepository);

            var resultedEssay = service.GetEssayDetails<EssayViewModel>(essay.Id);

            Assert.True(essay.UserId == resultedEssay.UserId, "GetEssayDetails method does not work correctly");
        }

        [Fact]
        public async Task GetEssaysInRangeTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.seeder.SeedManyEssaysAsync(context, 4);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var service = new EssayService(essayRepository);

            var resultedEssays = service.GetEssaysInRange<EssayViewModel>(CurrentPage, PageSize);

            Assert.True(resultedEssays.Count() == (context.Essays.Count() - PageSize), "GetEssaysInRange method does not work correctly");
        }

        [Fact]
        public async Task GetEssaysFromUserWithIdInRangeTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var userId = await this.seeder.SeedUserManyEssaysAsync(context, 4);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var service = new EssayService(essayRepository);

            var resultedEssays = service.GetEssaysFromUserWithIdInRange<EssayViewModel>(userId, CurrentPage, PageSize);

            Assert.True(resultedEssays.Count() == (context.Essays.Count() - PageSize), "GetEssaysFromUserWithIdInRange method does not work correctly");
        }

        [Fact]
        public async Task GetUserEssaysCountTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var expectedCount = 4;
            var userId = await this.seeder.SeedUserManyEssaysAsync(context, expectedCount);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var service = new EssayService(essayRepository);

            var resultedCount = service.GetUserEssaysCount(userId);

            Assert.True(resultedCount == expectedCount, "GetUserEssaysCount method does not work correctly");
        }

        [Fact]
        public async Task GetEssayCountTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var expectedCount = 4;
            var userId = await this.seeder.SeedUserManyEssaysAsync(context, expectedCount);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var service = new EssayService(essayRepository);

            var resultedCount = service.GetEssaysCount();

            Assert.True(resultedCount == expectedCount, "GetEssayCount method does not work correctly");
        }

        [Fact]
        public async Task GetEssayNameTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var seededEssay = await this.seeder.SeedEssayAsync(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var service = new EssayService(essayRepository);

            var resultedName = service.GetEssayName(seededEssay.UserId, seededEssay.ContestId);

            Assert.True(resultedName == seededEssay.Title, "GetEssayName method does not work correctly");
        }

        [Fact]
        public async Task GetEssayIdTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var seededEssay = await this.seeder.SeedEssayAsync(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var service = new EssayService(essayRepository);

            var resultedId = service.GetEssaysId(seededEssay.UserId, seededEssay.ContestId);

            Assert.True(resultedId == seededEssay.Id, "GetEssayId method does not work correctly");
        }

        [Fact]
        public async Task HasEssayWithIdTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var seededEssay = await this.seeder.SeedEssayAsync(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var service = new EssayService(essayRepository);

            var result = service.HasEssayWithId(seededEssay.Id);

            Assert.True(result == true, "HasEssayWithId method does not work correctly");
        }

        [Fact]
        public async Task HasUserEssayTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var seededEssay = await this.seeder.SeedEssayAsync(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var service = new EssayService(essayRepository);

            var result = service.HasUserEssay(seededEssay.UserId, seededEssay.ContestId);

            Assert.True(result == true, "HasUserEssay method does not work correctly");
        }

        [Fact]
        public async Task HasAnyGradedEssayTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var seededEssay = await this.seeder.SeedEssayAsync(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var service = new EssayService(essayRepository);

            var result = service.HasAnyGradedEssay();

            Assert.True(result == false, "HasAnyGradedEssay method does not work correctly");
        }
    }
}
