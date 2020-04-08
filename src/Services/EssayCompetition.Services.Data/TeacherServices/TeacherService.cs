namespace EssayCompetition.Services.Data.TeacherServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class TeacherService : ITeacherService
    {
        private readonly IDeletableEntityRepository<Essay> essaysRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IRepository<Grade> gradeRepository;

        public TeacherService(IDeletableEntityRepository<Essay> essaysRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IRepository<Grade> gradeRepository) 
        {
            this.essaysRepository = essaysRepository;
            this.categoryRepository = categoryRepository;
            this.gradeRepository = gradeRepository;
        }

        public IEnumerable<T> GetAllAvilableCategories<T>()
        {
            return this.categoryRepository.All().To<T>();
        }

        public T GetEssayInfo<T>(int essayId)
        {
            return this.essaysRepository.All().Where(x => x.Id == essayId).AsQueryable().To<T>().First();
        }

        public IEnumerable<T> GetTeacherNotReviewedEssays<T>(string userId)
        {
            return this.essaysRepository.All().Where(x => x.UserId == userId && x.Graded == false).AsQueryable().To<T>();
        }

        public async Task GradeEssayAsync(string privateComment, int points, int essayId)
        {
            Grade grade = new Grade()
            {
                EssayId = essayId,
                Points = points,
                PrivateComments = privateComment,
            };

            await this.gradeRepository.AddAsync(grade);
            await this.gradeRepository.SaveChangesAsync();
        }

        public bool HasEssayWithId(int essayId)
        {
            return this.essaysRepository.All().Any(x => x.Id == essayId);
        }

        public async Task<bool> UpdateEssayAync(UpdateEssayModel updateEssayModel)
        {
            //var es = updateEssayModel.To<Essay>(); 
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
                Graded = true,
            };
            return essay;
        }
    }
}
