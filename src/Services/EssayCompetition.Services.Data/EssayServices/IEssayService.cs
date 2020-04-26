namespace EssayCompetition.Services.Data.EssayServices
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IEssayService
    {
        IEnumerable<T> GetEssaysFromUserWithIdInRange<T>(string userId, int currentPage, int pageSize);

        IEnumerable<T> GetEssaysInRange<T>(int currentPage, int pageSize);

        int GetUserEssaysCount(string userId);

        int GetEssaysCount();

        int GetEssaysId(string contestanId, int contestId);

        bool HasUserEssay(string contestanId, int contestId);

        T GetEssayDetails<T>(int essayId);

        string GetEssayName(string contestanId, int contestId);

        bool HasEssayWithId(int essayId);

        IEnumerable<T> GetBestEssaysFromLastContest<T>(int contestId, IEnumerable<int> essaysIdsOrderedByPoints);
    }
}
