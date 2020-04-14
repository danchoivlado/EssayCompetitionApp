namespace EssayCompetition.Services.Data.ContestServices
{
    using System.Collections.Generic;

    public interface IContestService
    {
        IEnumerable<T> GetAllContests<T>();

        IEnumerable<T> GetAllContestsRange<T>(int currentPage, int pageSize);

        int GetContestsCount();
    }
}
