namespace EssayCompetition.Services.Data.TeacherReviewedServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.TeacherServices;
    using EssayCompetition.Services.Mapping;

    public class TeacherReviewedService : ITeacherReviewedService
    {
        private readonly IDeletableEntityRepository<Essay> essaysRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IRepository<Grade> gradeRepository;

        public TeacherReviewedService(IDeletableEntityRepository<Essay> essaysRepository, IDeletableEntityRepository<Category> categoryRepository,
            IRepository<Grade> gradeRepository)
        {
            this.essaysRepository = essaysRepository;
            this.categoryRepository = categoryRepository;
            this.gradeRepository = gradeRepository;
        }

        public IEnumerable<T> GetAllReviewedEssayFromTecher<T>(string teacherId)
        {
            return this.essaysRepository.All().Where(x => x.Graded && x.TeacherId == teacherId).AsQueryable().To<T>();
        }

        public T GetEssayInfo<T>(int essayId)
        {
            return this.essaysRepository.All().Where(x => x.Id == essayId).AsQueryable().To<T>().First();
        }

        public bool HasEssayWithId(int id)
        {
            return this.essaysRepository.All().Any(x => x.Id == id);
        }

        public IEnumerable<T> GetAllAvilableCategories<T>()
        {
            return this.categoryRepository.All().To<T>();
        }

        public T GetGradeViewModel<T>(int essayId)
        {
            return this.gradeRepository.All().Where(x => x.EssayId == essayId).AsQueryable().To<T>().First();
        }

        public async Task<bool> UpdateEssayAync(UpdateEssayModel updateEssayModel)
        {
            try
            {
                this.essaysRepository.Update(this.GenerateEssay(updateEssayModel));
            }
            catch (System.Exception ex)
            {
                return false;
            }

            await this.essaysRepository.SaveChangesAsync();

            return true;
        }

        private Essay GenerateEssay(UpdateEssayModel updateEssayModel)
        {
            Essay essay = new Essay()
            {
                Id = updateEssayModel.Id,
                ImageUrl = updateEssayModel.ImageUrl,
                UserId = updateEssayModel.UserId,
                CategoryId = updateEssayModel.CategoryId,
                Title = updateEssayModel.Title,
                Description = updateEssayModel.Description,
                Content = updateEssayModel.Content,
                TeacherId = updateEssayModel.TeacherId,
                Graded = true,
            };
            return essay;
        }

        public async Task GradeEssayAsync(string privateComment, int points, int essayId)
        {
            var essay = this.gradeRepository.All().Where(x => x.EssayId == essayId).First();
            essay.PrivateComments = privateComment;
            essay.Points = points;

            await this.gradeRepository.SaveChangesAsync();
        }
    }
}
