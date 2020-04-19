namespace EssayCompetition.Services.Data.GradeServices
{
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

        public T GetGradeDetails<T>(int essayId)
        {
            return this.gradeRepository.All().Where(x => x.EssayId == essayId).To<T>().First();
        }
    }
}
