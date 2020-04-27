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

        [Fact]
        public async Task HasAnyContext()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var result = service.HasAnyContext();

            Assert.True(result == true, "HasAnyContext method does not work correctly");
        }

        [Fact]
        public async Task HasContextWithNameTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var seededContest = await this.SeedContestAsync(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var result = service.HasContextWithName(seededContest.Name);

            Assert.True(result == true, "HasContextWithNameTest method does not work correctly");
        }

        [Fact]
        public async Task GetContestIdTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var seededContest = await this.SeedContestAsync(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var resultId = service.GetContestId(seededContest.Name);

            Assert.True(seededContest.Id == resultId, "GetContestIdTest method does not work correctly");
        }

        [Fact]
        public async Task GetContestParticipantsCount()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var seedContestanContest = await this.SeedContestContestant(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var participantsCount = service.GetContestParticipantsCount(seedContestanContest.First().Contest.Id);

            Assert.True(participantsCount == seedContestanContest.Count(), "GetContestParticipantsCount method does not work correctly");
        }

        [Fact]
        public async Task GetContestNowId()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.SeedData(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var contestNow = service.HasContextNow(DateTime.Now);

            Assert.True(contestNow == true, "GetContestNowId method does not work correctly");
        }

        [Fact]
        public async Task IsUserAlreadySubmitedEssay()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var seededEssay = await this.SeedSubmitedEssay(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var essayRepository = new EfDeletableEntityRepository<Essay>(context);
            var essayTeacherRepository = new EfDeletableEntityRepository<EssayTeacher>(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var service = new ContestService(contestRepository, essayRepository, essayTeacherRepository, contestantContestRepository);

            var userSubmited = service.IsUserAlreadySubmitedEssay(seededEssay.UserId);

            Assert.True(userSubmited == true, "IsUserAlreadySubmitedEssay method does not work correctly");
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
                StartTime = DateTime.Now.ToUniversalTime(),
                EndTime = DateTime.Now.ToUniversalTime().AddDays(1),
                CategoryId = category.Id,
                Name = "Legion1",
            };

            context.Contests.Add(contest);
            await context.SaveChangesAsync();
        }

        private async Task<Essay> SeedSubmitedEssay(ApplicationDbContext context)
        {
            var contest = await this.SeedContestAsync(context);
            var user = await this.SeedUserAsync(context, "test@abv.bg");

            var essay = new Essay()
            {
                UserId = user.Id,
                ContestId = contest.Id,
            };

            context.Essays.Add(essay);
            await context.SaveChangesAsync();
            return essay;
        }

        private async Task<IEnumerable<ContestantContest>> SeedContestContestant(ApplicationDbContext context)
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

            var user = await this.SeedUserAsync(context, "Test@abv.bg");
            var user2 = await this.SeedUserAsync(context, "Test2@abv.bg");

            var contestantContest = new ContestantContest()
            {
                ContestantId = user.Id,
                ContestId = contest.Id,
            };
            var contestantContest2 = new ContestantContest()
            {
                ContestantId = user2.Id,
                ContestId = contest.Id,
            };
            context.ContestantContest.Add(contestantContest);
            await context.SaveChangesAsync();
            return context.ContestantContest;
        }

        private async Task<ApplicationUser> SeedUserAsync(ApplicationDbContext context, string email)
        {
            var user = new ApplicationUser() { Email = email };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
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

        private async Task<Contest> SeedContestAsync(ApplicationDbContext context)
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
                StartTime = DateTime.Now.ToUniversalTime(),
                EndTime = DateTime.Now.ToUniversalTime().AddDays(1),
                CategoryId = category.Id,
                Name = "Legion1",
            };

            context.Contests.Add(contest);
            await context.SaveChangesAsync();
            return contest;
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
