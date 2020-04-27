namespace EssayCompetition.Services.Data.TeacherServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITeacherService
    {
        bool HasEssayWithId(int essayId);

        int GetTeacherNotReviewedEssaysCount(string userId);

        IEnumerable<T> GetTeacherNotReviewedEssaysInRange<T>(string userId, int currentPage, int pageSize);

        T GetEssayInfo<T>(int essayId);

        IEnumerable<T> GetAllAvilableCategories<T>();

        Task<bool> UpdateEssayAync(UpdateEssayModel updateEssayModel);

        Task GradeEssayAsync(string privateComment, int points, int essayId);
    }
}
