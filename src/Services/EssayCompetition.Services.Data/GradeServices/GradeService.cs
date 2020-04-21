namespace EssayCompetition.Services.Data.GradeServices
{
    using System.Collections.Generic;
    using System.Linq;

    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class GradeService : IGradeService
    {
        private readonly IRepository<Grade> gradeRepository;

        public GradeService(IRepository<Grade> gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }

        public IEnumerable<int> GetEssaysIdsOrderedByPoints()
        {
            return this.gradeRepository.All().OrderByDescending(x => x.Points).Select(x => x.EssayId);
        }

        public T GetGradeDetails<T>(int essayId)
        {
            return this.gradeRepository.All().Where(x => x.EssayId == essayId).To<T>().First();
        }
    }
}
