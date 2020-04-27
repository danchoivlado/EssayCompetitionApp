using EssayCompetition.Data.Models;
using EssayCompetition.Data.Repositories;
using EssayCompetition.Services.Data.RolesServices;
using EssayCompetition.Services.Data.Tests.Common;
using EssayCompetition.Services.Mapping;
using EssayCompetition.Web.ViewModels.Administration.Roles;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EssayCompetition.Common;
using System.Linq;

namespace EssayCompetition.Services.Data.Tests
{
    public class RoleServiceTests
    {
        private Seeder seeder;

        public RoleServiceTests()
        {
            this.seeder = new Seeder();
            AutoMapperConfig.RegisterMappings(
                typeof(CreateViewModel).GetTypeInfo().Assembly,
                typeof(ApplicationRole).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(
                typeof(ApplicationRole).GetTypeInfo().Assembly,
                typeof(RoleViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CreateRoleAsyncTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var roleName = "Ninja";
            var roleViewModel = new CreateViewModel() { Name = roleName };
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var service = new RolesService(roleRepository);

            await service.CreateRoleAsync<CreateViewModel>(roleViewModel.ToQueryable());

            Assert.True(roleRepository.All().Any(x => x.Name == roleName),"CreateRoleAsync method does not work correctly");
        }

        [Fact]
        public async Task GetAllTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var roleName1 = "Ninja1";
            var roleName2 = "Ninja2";
            await this.seeder.SeedRoleAsync(context, roleName1);
            await this.seeder.SeedRoleAsync(context, roleName2);
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var service = new RolesService(roleRepository);

            var resultedRoles = service.GetAll<RoleViewModel>();

            Assert.True(resultedRoles.Any(x => x.Name == roleName1), "GetAll method does not work correctly");
            Assert.True(resultedRoles.Any(x => x.Name == roleName2), "GetAll method does not work correctly");
        }

        [Fact]
        public async Task HasRoleWithNameTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var roleName1 = "Ninja1";
            await this.seeder.SeedRoleAsync(context, roleName1);
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var service = new RolesService(roleRepository);

            var result = service.HasRoleWithName(roleName1);

            Assert.True(result == true, "HasRoleWithName method does not work correctly");
        }

        [Fact]
        public async Task HasRoleWithIdTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var roleName1 = "Ninja1";
            var role = await this.seeder.SeedRoleAsync(context, roleName1);
            var roleRepository = new EfDeletableEntityRepository<ApplicationRole>(context);
            var service = new RolesService(roleRepository);

            var result = service.HasRoleWithId(role.Id);

            Assert.True(result == true, "HasRoleWithId method does not work correctly");
        }
    }
}
