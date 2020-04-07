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
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public TeacherService(IDeletableEntityRepository<Essay> essaysRepository, IDeletableEntityRepository<Category> categoryRepository)
        {
            this.essaysRepository = essaysRepository;
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<T> GetAllAvilableCategories<T>()
        {
            return this.categoryRepository.All().To<T>();
        }

        public T GetEssayInfo<T>(int essayId)
        {
            return this.essaysRepository.All().Where(x => x.Id == essayId).AsQueryable().To<T>().First();
        }

        public IEnumerable<T> GetTeacherEssays<T>(string userId)
        {
            return this.essaysRepository.All().Where(x => x.UserId == userId).AsQueryable().To<T>();
        }

        public bool HasEssayWithId(int essayId)
        {
            return this.essaysRepository.All().Any(x => x.Id == essayId);
        }
    }
}
