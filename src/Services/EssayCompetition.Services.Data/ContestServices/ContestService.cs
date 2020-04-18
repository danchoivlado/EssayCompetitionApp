namespace EssayCompetition.Services.Data.ContestServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Common;
    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class ContestService : IContestService
    {
        private readonly IDeletableEntityRepository<Contest> contestRepository;
        private readonly IDeletableEntityRepository<Essay> essayRepository;
        private readonly IDeletableEntityRepository<EssayTeacher> teacherEssayRepository;

        public ContestService(
            IDeletableEntityRepository<Contest> contestRepository,
            IDeletableEntityRepository<Essay> essayRepository,
            IDeletableEntityRepository<EssayTeacher> teacherEssayRepository)
        {
            this.contestRepository = contestRepository;
            this.essayRepository = essayRepository;
            this.teacherEssayRepository = teacherEssayRepository;
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

        public bool HasContextNow(DateTime date)
        {
            date = date.ToUniversalTime();
            var contest = this.contestRepository.All().FirstOrDefault(x => x.StartTime.Date == date.Date);
            if (contest != null)
            {
                if (date.TimeOfDay >= contest.StartTime.TimeOfDay && date.TimeOfDay <= contest.EndTime.TimeOfDay)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public async Task SendContestEssayAsync(string title, string description, string content, string userId, IEnumerable<string> teachersIds)
        {
            var contestId = this.GetContestNowId(DateTime.Now);

            if (contestId == -1)
            {
                throw new KeyNotFoundException();
            }

            Essay essay = new Essay()
            {
                Title = title,
                Description = description,
                Content = content,
                ContestId = contestId,
                UserId = userId,
                Graded = false,
            };

            await this.essayRepository.AddAsync(essay);
            await this.essayRepository.SaveChangesAsync();
            await this.AddEssayTeacher(teachersIds, essay.Id);
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

        public bool IsUserAlreadySubmitedEssay(string userId)
        {
            var contestId = this.GetContestNowId(DateTime.Now);
            if (contestId == -1)
            {
                return false;
            }

            return this.essayRepository.All().Any(x => x.UserId == userId && x.ContestId == contestId);
        }

        public T NextContext<T>()
        {
            var nexContext = this.contestRepository.All().FirstOrDefault(x => x.EndTime >= DateTime.Now);
            if (nexContext == null)
            {
                return default(T);
            }

            return nexContext.ToQueryable().To<T>().First();
        }

        private async Task AddEssayTeacher(IEnumerable<string> teachersIds, int essayId)
        {
            var dic = new Dictionary<string, int>();
            var teachersAssigned = this.teacherEssayRepository.All().Select(x => x.TeacherId);

            foreach (var teacherId in teachersIds)
            {
                int workCounter = 0;
                foreach (var teacherAssgned in teachersAssigned.Where(x => x == teacherId))
                {
                    workCounter++;
                }

                dic.Add(teacherId, workCounter);
            }

            var teacherWithMinimalWorkId = dic.OrderBy(x => x.Value).First().Key;
            var essayTeacher = new EssayTeacher()
            {
                EssayId = essayId,
                TeacherId = teacherWithMinimalWorkId,
            };

            await this.teacherEssayRepository.AddAsync(essayTeacher);
            await this.teacherEssayRepository.SaveChangesAsync();
        }

        private int GetContestNowId(DateTime date)
        {
            date = date.ToUniversalTime();
            var contest = this.contestRepository.All().FirstOrDefault(x => x.StartTime.Date == date.Date);
            if (contest != null)
            {
                if (date.TimeOfDay >= contest.StartTime.TimeOfDay && date.TimeOfDay <= contest.EndTime.TimeOfDay)
                {
                    return contest.Id;
                }

                return -1;
            }

            return -1;
        }
    }
}
