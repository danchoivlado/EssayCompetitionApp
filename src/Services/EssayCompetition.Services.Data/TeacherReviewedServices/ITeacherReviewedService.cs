﻿namespace EssayCompetition.Services.Data.TeacherReviewedServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Services.Data.TeacherServices;

    public interface ITeacherReviewedService
    {
        int GetAllReviewedEssayFromTecherCount(string teacherId);

        IEnumerable<T> GetAllReviewedEssayFromTecherInRange<T>(string teacherId, int currentPage, int pageSize);

        T GetEssayInfo<T>(int essayId);

        bool HasEssayWithId(int id);

        T GetGradeViewModel<T>(int essayId);

        Task<bool> UpdateEssayAync(UpdateEssayModel updateEssayModel);

        Task GradeEssayAsync(string privateComment, int points, int essayId);
    }
}
