namespace EssayCompetition.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Data.Repositories;
    using EssayCompetition.Services.Data.CalendarServices;
    using EssayCompetition.Services.Data.Tests.Common;
    using Xunit;

    public class CalendarServiceTests
    {
        private Seeder seeder;

        public CalendarServiceTests()
        {
            this.seeder = new Seeder();
        }

        [Fact]
        public async Task GetCalendarInfoTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.seeder.SeedContestAsync(context);
            await this.seeder.SeedContestAsync(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var service = new CalendarService(contestRepository);

            var expectedEventsCount = 2;
            var result = service.GetCalendarInfo(DateTime.Now.Month, DateTime.Now.Year);

            Assert.True(result.Events.Count() == expectedEventsCount, "GetCalendarInfo method does not work correctly");
        }

        [Fact]
        public async Task GetDateTestWhenItIsFirstMonthOfTheYear()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.seeder.SeedContestAsync(context);
            await this.seeder.SeedContestAsync(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var service = new CalendarService(contestRepository);

            var monthNow = 1;
            var yearNow = 2001;
            var result = service.GetDate(monthNow, yearNow, false);

            Assert.True(result.Month == 12, "GetDate method does not work correctly");
            Assert.True(result.Year == 2000, "GetDate method does not work correctly");
        }

        [Fact]
        public async Task GetDateTestWhenItIsLastMonthOfTheYear()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            await this.seeder.SeedContestAsync(context);
            await this.seeder.SeedContestAsync(context);
            var contestRepository = new EfDeletableEntityRepository<Contest>(context);
            var service = new CalendarService(contestRepository);

            var monthNow = 12;
            var yearNow = 2001;
            var result = service.GetDate(monthNow, yearNow, true);

            Assert.True(result.Month == 1, "GetDate method does not work correctly");
            Assert.True(result.Year == 2002, "GetDate method does not work correctly");
        }
    }
}
