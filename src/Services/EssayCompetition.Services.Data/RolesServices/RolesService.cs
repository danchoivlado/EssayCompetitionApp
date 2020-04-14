namespace EssayCompetition.Services.Data.RolesServices
{
    using System.Collections.Generic;
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

        public IEnumerable<T> GetAll<T>()
        {
            return this.roleRepository.All().To<T>().ToList();
        }

        public bool HasRoleWithId(string roleId)
        {
            return this.roleRepository.All().Any(x => x.Id == roleId);
        }

        public bool HasRoleWithName(string roleName)
        {
            return this.roleRepository.All().Any(x => x.Name == roleName);
        }
    }
}
