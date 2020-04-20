namespace EssayCompetition.Services.Data.EssayServices
{
    using System.Collections.Generic;
    using System.Linq;

    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class EssayService : IEssayService
    {
        private readonly IDeletableEntityRepository<Essay> essayRepository;

        public EssayService(IDeletableEntityRepository<Essay> essayRepository)
        {
            this.essayRepository = essayRepository;
        }

        public T GetEssayDetails<T>(int essayId)
        {
            return this.essayRepository.All().Where(x => x.Id == essayId).To<T>().First();
        }

        public IEnumerable<T> GetEssaysFromUserWithIdInRange<T>(string userId, int currentPage, int pageSize)
        {
            return this.essayRepository.All().Skip((currentPage - 1) * pageSize).Take(pageSize).Where(x => x.UserId == userId).To<T>();
        }

        public int GetUserEssaysCount(string userId)
        {
            return this.essayRepository.All().Count(x => x.UserId == userId);
        }
    }
}
