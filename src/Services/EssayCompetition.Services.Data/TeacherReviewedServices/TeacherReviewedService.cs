﻿namespace EssayCompetition.Services.Data.TeacherReviewedServices
{
    using System;
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
        private readonly IDeletableEntityRepository<EssayTeacher> essayTeacherRepository;

        public TeacherReviewedService(
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

        public T GetEssayInfo<T>(int essayId)
        {
            return this.essaysRepository.All().Where(x => x.Id == essayId).AsQueryable().To<T>().First();
        }

        public bool HasEssayWithId(int id)
        {
            return this.essaysRepository.All().Any(x => x.Id == id);
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
            catch (System.Exception)
            {
                return false;
            }

            await this.essaysRepository.SaveChangesAsync();

            return true;
        }

        public async Task GradeEssayAsync(string privateComment, int points, int essayId)
        {
            var essay = this.gradeRepository.All().Where(x => x.EssayId == essayId).First();
            essay.PrivateComments = privateComment;
            essay.Points = points;

            await this.gradeRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllReviewedEssayFromTecherInRange<T>(string teacherId, int currentPage, int pageSize)
        {
            var allEssaysIds = this.essayTeacherRepository.All().Where(x => x.TeacherId == teacherId).Select(x => x.EssayId);
            var allEssays = this.essaysRepository.All().Where(x => x.Graded == true);
            var filtredEssays = new List<T>();

            foreach (var essayId in allEssaysIds)
            {
                foreach (var essay in allEssays.Where(x => x.Id == essayId).To<T>())
                {
                    filtredEssays.Add(essay);
                }
            }

            //var a = filtredEssays.Skip((currentPage - 1) * pageSize).Take(pageSize).AsQueryable().To<T>();
            return filtredEssays.Skip((currentPage - 1) * pageSize).Take(pageSize);
        }

        public int GetAllReviewedEssayFromTecherCount(string teacherId)
        {
            var allEssaysIds = this.essayTeacherRepository.All().Where(x => x.TeacherId == teacherId).Select(x => x.EssayId);
            var allEssays = this.essaysRepository.All().Where(x => x.Graded == true);
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
                ContestId = updateEssayModel.ContestId,
                Graded = true,
                CreatedOn = createdOn,
            };
            return essay;
        }
    }
}
