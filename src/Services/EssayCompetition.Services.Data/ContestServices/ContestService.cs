namespace EssayCompetition.Services.Data.ContestServices
{
    using System.Collections.Generic;
    using System.Linq;
    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class ContestService : IContestService
    {
        private readonly IDeletableEntityRepository<Contest> contestRepository;

        public ContestService(IDeletableEntityRepository<Contest> contestRepository)
        {
            this.contestRepository = contestRepository;
        }

        public IEnumerable<T> GetAllContests<T>()
        {
            return this.contestRepository.All().To<T>();
        }

        public IEnumerable<T> GetAllContestsRange<T>(int currentPage, int pageSize)
        {
            return this.contestRepository.All().Skip((currentPage - 1) * pageSize).Take(pageSize).To<T>();
        }

        public int GetContestsCount()
        {
            return this.contestRepository.All().Count();
        }
    }
}
