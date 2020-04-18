namespace EssayCompetition.Services.Data.SignServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;

    public class SignService : ISignService
    {
        private readonly IDeletableEntityRepository<ContestantContest> contestantContestRepository;
        private readonly IDeletableEntityRepository<Contest> contestRepository;

        public SignService(
            IDeletableEntityRepository<ContestantContest> contestantContestRepository,
            IDeletableEntityRepository<Contest> contestRepository)
        {
            this.contestantContestRepository = contestantContestRepository;
            this.contestRepository = contestRepository;
        }

        public string GetContestName(int id)
        {
            return this.contestRepository.All().First(x => x.Id == id).Name;
        }

        public int GetNextContestId()
        {
            var time = DateTime.Now.ToUniversalTime();

            var nextContest = this.contestRepository.All().FirstOrDefault(x => x.EndTime >= time);
            if (nextContest == null)
            {
                return -1;
            }

            return nextContest.Id;
        }

        public async Task RegisterForContestAsync(string userId, int contestId)
        {
            ContestantContest contestantContest = new ContestantContest()
            {
                ContestantId = userId,
                ContestId = contestId,
            };

            await this.contestantContestRepository.AddAsync(contestantContest);
            await this.contestantContestRepository.SaveChangesAsync();
        }

        public bool UserAlreadyRegisteredForCompetition(string userId, int contestId)
        {
            return this.contestantContestRepository.All().Any(x => x.ContestantId == userId && x.ContestId == contestId);
        }
    }
}
