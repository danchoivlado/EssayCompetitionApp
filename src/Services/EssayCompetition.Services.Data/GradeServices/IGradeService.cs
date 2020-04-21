namespace EssayCompetition.Services.Data.GradeServices
{
    using System.Collections.Generic;

    public interface IGradeService
    {
        T GetGradeDetails<T>(int essayId);

        IEnumerable<int> GetEssaysIdsOrderedByPoints();
    }
}
