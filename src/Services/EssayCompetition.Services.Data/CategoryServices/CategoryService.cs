using EssayCompetition.Data.Common.Repositories;
using EssayCompetition.Data.Models;
using EssayCompetition.Services.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace EssayCompetition.Services.Data.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> deletableEntityRepository;

        public CategoryService(IDeletableEntityRepository<Category> deletableEntityRepository)
        {
            this.deletableEntityRepository = deletableEntityRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.deletableEntityRepository.All().To<T>().ToList();
        }

        public int GetCount()
        {
            return this.deletableEntityRepository.All().Count();
        }
    }
}
