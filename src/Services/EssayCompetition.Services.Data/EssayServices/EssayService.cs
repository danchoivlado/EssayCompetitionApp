namespace EssayCompetition.Services.Data.EssayServices
{
    using System.Collections.Generic;
    using System.Linq;

    using EssayCompetition.Common;
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

        public IEnumerable<T> GetBestEssaysFromLastContest<T>(int contestId, IEnumerable<int> essaysIdsOrderedByPoints)
        {
            var selectedEssays = this.essayRepository.All().Where(x => x.Graded && x.ContestId == contestId);
            var bestEssays = new List<T>();

            foreach (var essayId in essaysIdsOrderedByPoints)
            {
                var curEssay = selectedEssays.Where(x => x.Id == essayId);
                if (curEssay.Count() != 0)
                {
                    var a = curEssay.To<T>();
                    bestEssays.Add(curEssay.To<T>().First());

                    if (bestEssays.Count >= GlobalConstants.BestEssaysCount)
                    {
                        break;
                    }
                }
            }

            return bestEssays;
        }

        public T GetEssayDetails<T>(int essayId)
        {
            return this.essayRepository.All().Where(x => x.Id == essayId).To<T>().First();
        }

        public IEnumerable<T> GetEssaysInRange<T>(int currentPage, int pageSize)
        {
            return this.essayRepository.All().Skip((currentPage - 1) * pageSize).Take(pageSize).
                OrderByDescending(x => x.Contest.StartTime).To<T>();
        }

        public IEnumerable<T> GetEssaysFromUserWithIdInRange<T>(string userId, int currentPage, int pageSize)
        {
            return this.essayRepository.All().Skip((currentPage - 1) * pageSize).Take(pageSize).Where(x => x.UserId == userId).To<T>();
        }

        public int GetUserEssaysCount(string userId)
        {
            return this.essayRepository.All().Count(x => x.UserId == userId);
        }

        public int GetEssaysCount()
        {
            return this.essayRepository.All().Count();
        }

        public string GetEssayName(string contestanId, int contestId)
        {
            return this.essayRepository.All().First(x => x.ContestId == contestId && x.UserId == contestanId).Title;
        }

        public int GetEssaysId(string contestanId, int contestId)
        {
            return this.essayRepository.All().First(x => x.ContestId == contestId && x.UserId == contestanId).Id;
        }
    }
}
