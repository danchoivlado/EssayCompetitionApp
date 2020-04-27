namespace EssayCompetition.Services.Data.Tests.Common
{
    using System;

    using EssayCompetition.Data;
    using Microsoft.EntityFrameworkCore;

    public class EssayCompetitionContextInMemoryFactory
    {
        public static ApplicationDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
