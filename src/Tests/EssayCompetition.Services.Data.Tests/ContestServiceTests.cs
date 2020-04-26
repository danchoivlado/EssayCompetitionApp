using EssayCompetition.Data;
using EssayCompetition.Data.Models;
using EssayCompetition.Data.Repositories;
using EssayCompetition.Services.Data.ContestServices;
using EssayCompetition.Services.Data.Tests.Common;
using EssayCompetition.Services.Mapping;
using EssayCompetition.Web.ViewModels.Administration.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EssayCompetition.Web.ViewModels.Administration.Contest;

namespace EssayCompetition.Services.Data.Tests
{
    public class ContestServiceTests
    {
        public ContestServiceTests()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(Contest).GetTypeInfo().Assembly,
                typeof(ContestViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task CountTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var countBefore = service.GetContestsCount();
            await service.AddContestAsync(DateTime.Now, DateTime.Now, "Legion2", "Legion", context.Categories.First().Id);
            var countAfter = service.GetContestsCount();

            Assert.True(countAfter == countBefore + 1, "Count method does not work correctly");
        }

        [Fact]
        public async Task GetAllTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var count = service.GetContestsCount();
            var allContests = service.GetAllContests<ContestViewModel>();

            Assert.True(count == allContests.Count(), "GetAllTest method does not work correctly");
        }

        [Fact]
        public async Task GetAllInRangeTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var count = service.GetContestsCount();
            var allContests = service.GetAllContestsRange<ContestViewModel>(1, count - 1);
            var countGetCategories = allContests.Count();

            Assert.True(countGetCategories == count - 1, "GetAllInRangeTest method does not work correctly");
        }

        [Fact]
        public async Task GetContestDetails()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var contestId = context.Contests.FirstOrDefault().Id;
            var contestDetails = service.GetContestDetails<ContestViewModel>(contestId);

            Assert.True(contestDetails != null, "GetContestDetails method does not work correctly");
        }

        [Fact]
        public async Task HasContestWithIdTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var contestId = context.Contests.FirstOrDefault().Id;
            var currentContest = service.HasContestWithId(contestId);

            Assert.True(currentContest != null, "HasContestWithIdTest method does not work correctly");
        }

        [Fact]
        public async Task HasContesNow()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var contestNow = service.HasContextNow(DateTime.Now);

            Assert.True(contestNow != null, "HasContesNow method does not work correctly");
        }

        [Fact]
        public async Task UpdateContestAsyncTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var contestId = context.Contests.FirstOrDefault().Id;
            var contestCategory = context.Contests.FirstOrDefault().CategoryId;
            await service.UpdateContestAsync(DateTime.Now, DateTime.Now, "*", "*", contestCategory, contestId);
            var contestAfterName = service.GetContestName(contestId);

            Assert.True(contestAfterName == "*", "HasContesNow method does not work correctly");
        }

        [Fact]
        public async Task NextContextTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var contestRetId = await this.SeedNextContest(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var result = service.NextContext<ContestViewModel>();

            Assert.True(contestRetId == result.Id, "NextContextTest method does not work correctly");
        }

        [Fact]
        public async Task GetLastContestIdTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var contestRetId = await this.SeedLastContest(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var resultId = service.GetLastContestId();

            Assert.True(contestRetId == resultId, "GetLastContestIdTest method does not work correctly");
        }

        private async Task SeedData(ApplicationDbContext context)
        {
            var category = new Category()
            {
                Title = "Basic",
                Description = "No rules",
                ImageUrl = "#",
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var contest = new Contest()
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                CategoryId = category.Id,
                Name = "Legion1",
            };

            context.Contests.Add(contest);
            await context.SaveChangesAsync();
        }

        private async Task<int> SeedNextContest (ApplicationDbContext context)
        {
            var category = new Category()
            {
                Title = "Basic",
                Description = "No rules",
                ImageUrl = "#",
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var contest = new Contest()
            {
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(2),
                CategoryId = category.Id,
            };

            context.Contests.Add(contest);
            await context.SaveChangesAsync();
            return contest.Id;
        }

        private async Task<int> SeedLastContest(ApplicationDbContext context)
        {
            var category = new Category()
            {
                Title = "Basic",
                Description = "No rules",
                ImageUrl = "#",
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var contest = new Contest()
            {
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now.AddDays(-2),
                CategoryId = category.Id,
            };

            context.Contests.Add(contest);
            await context.SaveChangesAsync();
            return contest.Id;
        }
    }
}
