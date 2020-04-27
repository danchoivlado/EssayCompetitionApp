namespace EssayCompetition.Services.Data.Tests
{
    using System.Reflection;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Data.Repositories;
    using EssayCompetition.Services.Data.Tests.Common;
    using EssayCompetition.Services.Data.UserAdditionalInfoServices;
    using EssayCompetition.Services.Mapping;
    using EssayCompetition.Web.ViewModels.Identity;
    using Xunit;

    public class UserAdditionalInfoServiceTests
    {
        private Seeder seeder;

        public UserAdditionalInfoServiceTests()
        {
            this.seeder = new Seeder();
            AutoMapperConfig.RegisterMappings(
                typeof(UserAdditionalInfo).GetTypeInfo().Assembly,
                typeof(IndexViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task GetEssayInfoTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user = await this.seeder.SeedUserAsync(context, "test@abv.bg");
            var userAdditionalInfo = await this.seeder.SeedUserAdditionalInfo(context, user.Id, "#");
            var userAdditionalInfoRepository = new EfDeletableEntityRepository<UserAdditionalInfo>(context);
            var service = new UserAdditionalInfoService(userAdditionalInfoRepository);

            var result = service.GetUserProfilePicture(user.Id);

            Assert.True(result != null, "GetEssayInfo method does not work correctly");
        }

        [Fact]
        public async Task GetUserWithIdAdditionalInfoTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user = await this.seeder.SeedUserAsync(context, "test@abv.bg");
            var userAdditionalInfo = await this.seeder.SeedUserAdditionalInfo(context, user.Id, "#");
            var userAdditionalInfoRepository = new EfDeletableEntityRepository<UserAdditionalInfo>(context);
            var service = new UserAdditionalInfoService(userAdditionalInfoRepository);

            var result = service.GetUserWithIdAdditionalInfo<IndexViewModel>(user.Id);

            Assert.True(result.UserId == user.Id, "GetUserWithIdAdditionalInfo method does not work correctly");
        }

        [Fact]
        public async Task HasUserAdditionalInfoWithIdShouldReturnTrue()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user = await this.seeder.SeedUserAsync(context, "test@abv.bg");
            var userAdditionalInfo = await this.seeder.SeedUserAdditionalInfo(context, user.Id, "#");
            var userAdditionalInfoRepository = new EfDeletableEntityRepository<UserAdditionalInfo>(context);
            var service = new UserAdditionalInfoService(userAdditionalInfoRepository);

            var result = service.HasUserAdditionalInfoWithId(user.Id);

            Assert.True(result, "HasUserAdditionalInfoWithId method does not work correctly");
        }

        [Fact]
        public async Task HasUserAdditionalInfoWithIdShouldReturnFalse()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user = await this.seeder.SeedUserAsync(context, "test@abv.bg");
            var userAdditionalInfoRepository = new EfDeletableEntityRepository<UserAdditionalInfo>(context);
            var service = new UserAdditionalInfoService(userAdditionalInfoRepository);

            var result = service.HasUserAdditionalInfoWithId(user.Id);

            Assert.False(result, "HasUserAdditionalInfoWithId method does not work correctly");
        }
    }
}
