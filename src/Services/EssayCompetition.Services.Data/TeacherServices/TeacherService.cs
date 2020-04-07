namespace EssayCompetition.Services.Data.TeacherServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class TeacherService : ITeacherService
    {
        private readonly IDeletableEntityRepository<Essay> essaysRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;

        public TeacherService(
            IDeletableEntityRepository<Essay> essaysRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<ApplicationRole> roleRepository)
        {
            this.essaysRepository = essaysRepository;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public IEnumerable<T> GetTeacherEssays<T>(string userId)
        {
            return this.essaysRepository.All().Where(x => x.UserId == userId).AsQueryable().To<T>();
        }
    }
}
