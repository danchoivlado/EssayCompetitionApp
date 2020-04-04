namespace EssayCompetition.Services.Data.UsersServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public async Task DeleteUserAsync(string userId)
        {
            var user = this.userRepository.All().First(x => x.Id == userId);
            this.userRepository.Delete(user);
            await this.userRepository.SaveChangesAsync();
        }

        public T GetUserById<T>(string id)
        {
            return this.userRepository.All().Where(x => x.Id == id).To<T>().First();
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

        public IEnumerable<T> GetUsersWithRoles<T>(int currentPage, int pageSize, string searchString)
        {
            var results = this.userRepository.All();

            if (!string.IsNullOrEmpty(searchString))
            {
                results = results.Where(x => x.UserName.Contains(searchString));
            }

            return results.Skip((currentPage - 1) * pageSize).Take(pageSize)
                .To<T>().ToList();
        }

        public bool HasUserWithId(string id)
        {
            return this.userRepository.All().Any(x => x.Id == id);
        }

        public async Task UpdateUserAsync(string userId, string userName, string email)
        {
            var user = this.userRepository.All().First(x => x.Id == userId);
            user.UserName = userName;
            user.Email = email;

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
        }

        private string GetRoleName(string roleId)
        {
            return this.roleRepository.All().First(x => x.Id == roleId).Name;
        }
    }
}
