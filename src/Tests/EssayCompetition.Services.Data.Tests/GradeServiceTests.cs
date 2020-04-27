using EssayCompetition.Data.Models;
using EssayCompetition.Data.Repositories;
using EssayCompetition.Services.Data.GradeServices;
using EssayCompetition.Services.Data.Tests.Common;
using EssayCompetition.Services.Mapping;
using EssayCompetition.Web.ViewModels.Contest.MyEssay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EssayCompetition.Services.Data.Tests
{
    public class GradeServiceTests
    {
        private Seeder seeder;

        public GradeServiceTests()
        {
            this.seeder = new Seeder();
            AutoMapperConfig.RegisterMappings(
                typeof(Grade).GetTypeInfo().Assembly,
                typeof(GradeViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task GetGradeDetailsTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var grade = await this.seeder.AddGradeAsync(context, essay.Id);
            var gradeRepository = new EfRepository<Grade>(context);
            var service = new GradeService(gradeRepository);

            var resultedgrade = service.GetGradeDetails<GradeViewModel>(essay.Id);

            Assert.True(resultedgrade.Points == grade.Points, "GetGradeDetails method does not work correctly");
            Assert.True(resultedgrade.PrivateComments == grade.PrivateComments, "GetGradeDetails method does not work correctly");
        }

        [Fact]
        public async Task GetEssayPointsTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var grade = await this.seeder.AddGradeAsync(context, essay.Id);
            var gradeRepository = new EfRepository<Grade>(context);
            var service = new GradeService(gradeRepository);

            var resultedPoints = service.GetEssayPoints(essay.Id);

            Assert.True(resultedPoints == grade.Points, "GetEssayPoints method does not work correctly");
        }

        [Fact]
        public async Task EssayGradedWithoutGrade()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var gradeRepository = new EfRepository<Grade>(context);
            var service = new GradeService(gradeRepository);

            var result = service.EssayGradet(essay.Id);

            Assert.True(result == false, "EssayGraded method does not work correctly");
        }

        [Fact]
        public async Task EssayGradedWithGrade()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var grade = await this.seeder.AddGradeAsync(context, essay.Id);
            var gradeRepository = new EfRepository<Grade>(context);
            var service = new GradeService(gradeRepository);

            var result = service.EssayGradet(essay.Id);

            Assert.True(result == true, "EssayGraded method does not work correctly");
        }

        [Fact]
        public async Task GetEssaysIdsOrderedByPointsTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay1 = await this.seeder.SeedEssayAsync(context);
            var essay2 = await this.seeder.SeedEssayAsync(context);
            var essay3 = await this.seeder.SeedEssayAsync(context);
            var grade1 = await this.seeder.AddGradeAsync(context, essay1.Id, 22);
            var grade2 = await this.seeder.AddGradeAsync(context, essay2.Id, 33);
            var grade3 = await this.seeder.AddGradeAsync(context, essay3.Id, 44);
            var gradeRepository = new EfRepository<Grade>(context);
            var service = new GradeService(gradeRepository);

            var expectSorted = gradeRepository.All().OrderByDescending(x => x.Points).Select(x => x.Id);
            var resultGrades = service.GetEssaysIdsOrderedByPoints();

            Assert.Equal<IEnumerable<int>>(expectSorted, resultGrades);
        }
    }
}
