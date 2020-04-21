namespace EssayCompetition.Services.Data.ContestServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContestService
    {
        bool IsUserAlreadySubmitedEssay(string userId);

        IEnumerable<T> GetAllContests<T>();

        IEnumerable<T> GetAllContestsRange<T>(int currentPage, int pageSize);

        int GetContestsCount();

        T GetContestDetails<T>(int id);

        string GetContestName(int contestId);

        IEnumerable<string> GetContestParticipantsIds(int contestId);

        bool HasContestWithId(int id);

        Task UpdateContestAsync(DateTime start, DateTime end, string name, string description, int categoryId, int id);

        Task AddContestAsync<T>(DateTime start, DateTime end, string name, string description, int categoryId);

        bool HasContextNow(DateTime date);

        T NextContext<T>();

        Task SendContestEssayAsync(string title, string description, string content, string userId, IEnumerable<string> teachersIds);

        int GetLastContestId();
    }
}
