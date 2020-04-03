
namespace EssayCompetition.Services.Data.RolesServices
{
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class RolesService : IRolesService
    {
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;

        public RolesService(IDeletableEntityRepository<ApplicationRole> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task CreateRoleAsync<T>(IQueryable<T> role)
        {
            var createdRole = role.To<ApplicationRole>().First();
            await this.roleRepository.AddAsync(createdRole);
            await this.roleRepository.SaveChangesAsync();
        }
    }
}
