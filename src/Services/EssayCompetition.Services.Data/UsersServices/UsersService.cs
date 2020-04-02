namespace EssayCompetition.Services.Data.UsersServices
{
    using System.Collections.Generic;
    using System.Linq;

    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository, IDeletableEntityRepository<ApplicationRole> roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public IEnumerable<string> GetUserRolesNames(IEnumerable<string> rolesIds)
        {
            var roleNamesList = new List<string>();

            foreach (var rolesId in rolesIds)
            {
                roleNamesList.Add(this.GetRoleName(rolesId));
            }

            return roleNamesList;
        }

        public int GetUsersCount()
        {
            return this.userRepository.All().Count();
        }

        public IEnumerable<T> GetUsersWithRoles<T>()
        {
            return this.userRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetUsersWithRoles<T>(int currentPage, int pageSize)
        {
            return this.userRepository.All().Skip((currentPage - 1) * pageSize).Take(pageSize)
                .To<T>().ToList();
        }

        private string GetRoleName(string roleId)
        {
            return this.roleRepository.All().First(x => x.Id == roleId).Name;
        }
    }
}
