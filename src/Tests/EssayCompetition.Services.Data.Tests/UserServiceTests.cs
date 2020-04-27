using EssayCompetition.Data.Models;
using EssayCompetition.Data.Repositories;
using EssayCompetition.Services.Data.Tests.Common;
using EssayCompetition.Services.Data.UsersServices;
using EssayCompetition.Services.Mapping;
using EssayCompetition.Web.ViewModels.ContestHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EssayCompetition.Web.ViewModels.Administration.Roles;

namespace EssayCompetition.Services.Data.Tests
{
    public class UserServiceTests
    {
        private Seeder seeder;

        public UserServiceTests()
        {
            this.seeder = new Seeder();
            AutoMapperConfig.RegisterMappings(
                typeof(ApplicationUser).GetTypeInfo().Assembly,
                typeof(ContestantViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(
                typeof(ApplicationUser).GetTypeInfo().Assembly,
                typeof(DetailsViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task GetEssayInfoTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user = await this.seeder.SeedUserAsync(context,"test@abv.bg");
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var service = new UsersService(userRepository,roleRepository);

            var result = service.HasUserWithId(user.Id);

            Assert.True(result, "GetEssayInfo method does not work correctly");
        }

        [Fact]
        public async Task HasDeletedUserWithIdTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user = await this.seeder.SeedDeletedUserAsync(context, "test@abv.bg");
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var service = new UsersService(userRepository, roleRepository);

            var result = service.HasDeletedUserWithId(user.Id);

            Assert.True(result, "HasDeletedUserWithId method does not work correctly");
        }

        [Fact]
        public async Task UnDeleteUserAsyncTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user = await this.seeder.SeedDeletedUserAsync(context, "test@abv.bg");
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var service = new UsersService(userRepository, roleRepository);

            await service.UnDeleteUserAsync(user.Id);
            var result = user.IsDeleted;

            Assert.False(result, "UnDeleteUserAsync method does not work correctly");
        }

        [Fact]
        public async Task GetUsersWithIdsTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user1 = await this.seeder.SeedDeletedUserAsync(context, "test1@abv.bg");
            var user2 = await this.seeder.SeedDeletedUserAsync(context, "test2@abv.bg");
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var service = new UsersService(userRepository, roleRepository);
            var userIds = new List<string>() { user1.Id, user2.Id };

            var result = service.GetUsersWithIds<ContestantViewModel>(userIds);

            Assert.False(result.Count() == userIds.Count(), "GetUsersWithIds method does not work correctly");
        }

        [Fact]
        public async Task GetUsersCountTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user1 = await this.seeder.SeedDeletedUserAsync(context, "test1@abv.bg");
            var user2 = await this.seeder.SeedDeletedUserAsync(context, "test2@abv.bg");
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var service = new UsersService(userRepository, roleRepository);
            var userIds = new List<string>() { user1.Id, user2.Id };

            var result = service.GetUsersCount();

            Assert.False(result == userIds.Count(), "GetUsersCount method does not work correctly");
        }

        [Fact]
        public async Task GetUserByIdTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user1 = await this.seeder.SeedDeletedUserAsync(context, "test1@abv.bg");
            var user2 = await this.seeder.SeedDeletedUserAsync(context, "test2@abv.bg");
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var service = new UsersService(userRepository, roleRepository);

            var result = service.GetUserById<DetailsViewModel>(user1.Id);

            Assert.True(result.Id == user1.Id, "GetUserById method does not work correctly");
        }

        [Fact]
        public async Task DeleteUserAsyncTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var user1 = await this.seeder.SeedUserAsync(context, "test1@abv.bg");
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var service = new UsersService(userRepository, roleRepository);

            var preResult = user1.IsDeleted;
            await service.DeleteUserAsync(user1.Id);
            var result = user1.IsDeleted;

            Assert.True(result, "DeleteUserAsync method does not work correctly");
            Assert.False(preResult, "DeleteUserAsync method does not work correctly");
        }
    }
}
