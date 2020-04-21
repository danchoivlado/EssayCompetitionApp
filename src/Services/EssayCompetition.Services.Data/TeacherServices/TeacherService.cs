namespace EssayCompetition.Services.Data.TeacherServices
{
    using System;
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
        private readonly IDeletableEntityRepository<EssayTeacher> essayTeacherRepository;

        public TeacherService(
            IDeletableEntityRepository<Essay> essaysRepository,
            IDeletableEntityRepository<Category> categoryRepository,
            IRepository<Grade> gradeRepository,
            IDeletableEntityRepository<EssayTeacher> essayTeacherRepository)
        {
            this.essaysRepository = essaysRepository;
            this.categoryRepository = categoryRepository;
            this.gradeRepository = gradeRepository;
            this.essayTeacherRepository = essayTeacherRepository;
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
            var allEssaysIds = this.essayTeacherRepository.All().Where(x => x.TeacherId == userId).Select(x => x.EssayId);
            var allEssays = this.essaysRepository.All().Where(x => x.Graded != true);
            var filtredEssays = new List<Essay>();

            foreach (var essayId in allEssaysIds)
            {
                foreach (var essay in allEssays.Where(x => x.Id == essayId))
                {
                    filtredEssays.Add(essay);
                }
            }

            return filtredEssays.AsQueryable().To<T>();
        }

        public int GetTeacherNotReviewedEssaysCount(string userId)
        {
            var allEssaysIds = this.essayTeacherRepository.All().Where(x => x.TeacherId == userId).Select(x => x.EssayId);
            var allEssays = this.essaysRepository.All().Where(x => x.Graded != true);
            int counter = 0;

            foreach (var essayId in allEssaysIds)
            {
                foreach (var essay in allEssays.Where(x => x.Id == essayId))
                {
                    counter++;
                }
            }

            return counter;
        }

        public IEnumerable<T> GetTeacherNotReviewedEssaysInRange<T>(string userId, int currentPage, int pageSize)
        {
            var allEssaysIds = this.essayTeacherRepository.All().Where(x => x.TeacherId == userId).Select(x => x.EssayId);
            var allEssays = this.essaysRepository.All().Where(x => x.Graded != true);
            var filtredEssays = new List<T>();

            foreach (var essayId in allEssaysIds)
            {
                foreach (var essay in allEssays.Where(x => x.Id == essayId).AsQueryable().To<T>())
                {
                    filtredEssays.Add(essay);
                }
            }

            return filtredEssays.Skip((currentPage - 1) * pageSize).Take(pageSize);
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
            var createdOn = this.essaysRepository.AllAsNoTracking().First(x => x.Id == updateEssayModel.Id).CreatedOn;
            Essay essay = new Essay()
            {
                Id = updateEssayModel.Id,
                ImageUrl = updateEssayModel.ImageUrl,
                UserId = updateEssayModel.UserId,
                Title = updateEssayModel.Title,
                Description = updateEssayModel.Description,
                Content = updateEssayModel.Content,
                Graded = true,
                ContestId = updateEssayModel.ContestId,
                CreatedOn = createdOn,
            };
            return essay;
        }
    }
}
