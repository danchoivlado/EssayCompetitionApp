namespace EssayCompetition.Services.Data.EssayServices
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IEssayService
    {
        IEnumerable<T> GetEssaysFromUserWithIdInRange<T>(string userId, int currentPage, int pageSize);

        int GetUserEssaysCount(string userId);
    }
}
