namespace EssayCompetition.Services.Data.Tests.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EssayCompetition.Data;
    using EssayCompetition.Data.Models;

    public class Seeder
    {
        private const string TestEmail = "test@abv.bg";

        public async Task SeedContestAsync(ApplicationDbContext context)
        {
            var category = new Category()
            {
                Title = "Category1",
                Description = "Category1",
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

        public async Task<Contest> SeedFutureContestAsync(ApplicationDbContext context)
        {
            var category = new Category()
            {
                Title = "Category1",
                Description = "Category1",
                ImageUrl = "#",
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var contest = new Contest()
            {
                StartTime = DateTime.Now.ToUniversalTime().AddDays(1),
                EndTime = DateTime.Now.ToUniversalTime().AddDays(2),
                CategoryId = category.Id,
                Name = "Legion2",
            };

            context.Contests.Add(contest);
            await context.SaveChangesAsync();
            return contest;
        }

        public async Task<ApplicationUser> SeedUserAsync(ApplicationDbContext context, string userEmail)
        {
            var user = new ApplicationUser() { Email = userEmail, UserName = userEmail};

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<string>> SeedManyUserAsync(ApplicationDbContext context, int usersCount)
        {
            var userIds = new List<string>();
            for (int i = 0; i < usersCount; i++)
            {
                var user = new ApplicationUser() { Email = TestEmail };
                context.Users.Add(user);
                userIds.Add(user.Id);
            }

            await context.SaveChangesAsync();
            return userIds;
        }

        public async Task<ContestantContest> SeedContestantContestAsync(ApplicationDbContext context)
        {
            var user = await this.SeedUserAsync(context, TestEmail);
            var contest = await this.SeedFutureContestAsync(context);

            var contestantContest = new ContestantContest()
            {
                ContestantId = user.Id,
                ContestId = contest.Id,
            };

            context.ContestantContest.Add(contestantContest);
            await context.SaveChangesAsync();
            return contestantContest;
        }

        public async Task<Essay> SeedEssayAsync(ApplicationDbContext context)
        {
            var user = await this.SeedUserAsync(context, TestEmail);
            var contest = await this.SeedFutureContestAsync(context);

            Essay essay = new Essay()
            {
                ContestId = contest.Id,
                UserId = user.Id,
            };

            context.Essays.Add(essay);
            await context.SaveChangesAsync();
            return essay;
        }

        public async Task<Essay> SeedEssayAsync(ApplicationDbContext context, string userId, int contestId)
        {
            Essay essay = new Essay()
            {
                ContestId = contestId,
                UserId = userId,
                Graded = true,
            };

            context.Essays.Add(essay);
            await context.SaveChangesAsync();
            return essay;
        }

        public async Task SeedManyEssaysAsync(ApplicationDbContext context, int essaysCount)
        {
            var userIds = await this.SeedManyUserAsync(context, essaysCount);
            var contest = await this.SeedFutureContestAsync(context);

            foreach (var userId in userIds)
            {
                await this.SeedEssayAsync(context, userId, contest.Id);
            }

            await context.SaveChangesAsync();
        }

        public async Task<string> SeedUserManyEssaysAsync(ApplicationDbContext context, int essaysCount)
        {
            var user = await this.SeedUserAsync(context, TestEmail);
            var contest = await this.SeedFutureContestAsync(context);

            for (int i = 0; i < essaysCount; i++)
            {
                await this.SeedEssayAsync(context, user.Id, contest.Id);
            }

            await context.SaveChangesAsync();
            return user.Id;
        }
    }
}
