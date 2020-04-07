namespace EssayCompetition.Services.Data.TeacherServices
{
    using System.Collections.Generic;

    public interface ITeacherService
    {
        IEnumerable<T> GetTeacherEssays<T>(string userId);
    }
}
