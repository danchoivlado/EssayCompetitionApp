namespace EssayCompetition.Services.Data.ContestServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task AddContestAsync<T>(DateTime start, DateTime end, string name, string description, int categoryId)
        {
            Contest contest = new Contest()
            {
                StartTime = start.ToUniversalTime(),
                EndTime = end.ToUniversalTime(),
                Name = name,
                Description = description,
                CategoryId = categoryId,
            };

            await this.contestRepository.AddAsync(contest);
            await this.contestRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllContests<T>()
        {
            return this.contestRepository.All().To<T>();
        }

        public IEnumerable<T> GetAllContestsRange<T>(int currentPage, int pageSize)
        {
            return this.contestRepository.All().Skip((currentPage - 1) * pageSize).Take(pageSize).To<T>();
        }

        public T GetContestDetails<T>(int id)
        {
            return this.contestRepository.All().Where(x => x.Id == id).AsQueryable().To<T>().First();
        }

        public int GetContestsCount()
        {
            return this.contestRepository.All().Count();
        }

        public bool HasContestWithId(int id)
        {
            return this.contestRepository.All().Any(x => x.Id == id);
        }

        public async Task UpdateContestAsync(DateTime start, DateTime end, string name, string description, int categoryId, int id)
        {
            var contest = new Contest()
            {
                Id = id,
                StartTime = start.ToUniversalTime(),
                EndTime = end.ToUniversalTime(),
                Name = name,
                Description = description,
                CategoryId = categoryId,
            };

            this.contestRepository.Update(contest);
            await this.contestRepository.SaveChangesAsync();
        }
    }
}
