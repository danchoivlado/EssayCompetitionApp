namespace EssayCompetition.Services.Data.ContestServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IContestService
    {
        IEnumerable<T> GetAllContests<T>();

        IEnumerable<T> GetAllContestsRange<T>(int currentPage, int pageSize);

        int GetContestsCount();

        T GetContestDetails<T>(int id);

        bool HasContestWithId(int id);

        Task UpdateContestAsync(DateTime start, DateTime end, string name, string description, int categoryId, int id);

        Task AddContestAsync<T>(DateTime start, DateTime end, string name, string description, int categoryId);

        bool HasContextNow(DateTime date);
    }
}
