namespace EssayCompetition.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Data.Repositories;
    using EssayCompetition.Services.Data.SignServices;
    using EssayCompetition.Services.Data.Tests.Common;
    using Xunit;

    public class SignServiceTests
    {
        private Seeder seeder;

        public SignServiceTests()
        {
            this.seeder = new Seeder();
        }

        [Fact]
        public async Task GetContestNameTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.seeder.SeedContestAsync(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var service = new SignService(contestantContestRepository, contestRepository);

            var currentContext = context.Contests.First();
            var reusltedName = service.GetContestName(currentContext.Id);

            Assert.True(currentContext.Name == reusltedName, "GetContestName method does not work correctly");
        }

        [Fact]
        public async Task GetNextContestIdTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var futureContest = await this.seeder.SeedFutureContestAsync(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var service = new SignService(contestantContestRepository, contestRepository);

            var reusltedId = service.GetNextContestId();

            Assert.True(reusltedId == futureContest.Id, "GetNextContestId method does not work correctly");
        }

        [Fact]
        public async Task RegisterForContestTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var futureContext = await this.seeder.SeedFutureContestAsync(context);
            var user = await this.seeder.SeedUserAsync(context, "test@abv.bg");
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var service = new SignService(contestantContestRepository, contestRepository);

            await service.RegisterForContestAsync(user.Id, futureContext.Id);
            var contest = context.ContestantContest.FirstOrDefault();

            Assert.True(contest != null, "RegisterForContest method does not work correctly");
            Assert.True(contest.ContestantId == user.Id, "RegisterForContest method does not work correctly");
            Assert.True(contest.ContestId == futureContext.Id, "RegisterForContest method does not work correctly");
        }

        [Fact]
        public async Task UserAlreadyRegisteredForCompetitionTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var contestantContest = await this.seeder.SeedContestantContestAsync(context);
            var contestantContestRepository = new EfDeletableEntityRepository<ContestantContest>(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var service = new SignService(contestantContestRepository, contestRepository);

            var result = service.UserAlreadyRegisteredForCompetition(contestantContest.ContestantId, contestantContest.ContestId);

            Assert.True(result == true, "UserAlreadyRegisteredForCompetition method does not work correctly");
        }
    }
}
