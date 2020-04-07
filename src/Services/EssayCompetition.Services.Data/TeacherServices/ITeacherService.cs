namespace EssayCompetition.Services.Data.TeacherServices
{
    using System.Collections.Generic;

    public interface ITeacherService
    {
        bool HasEssayWithId(int essayId);

        IEnumerable<T> GetTeacherEssays<T>(string userId);

        T GetEssayInfo<T>(int essayId);

        IEnumerable<T> GetAllAvilableCategories<T>();
    }
}
