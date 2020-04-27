namespace EssayCompetition.Services.Data.Tests
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Data.Repositories;
    using EssayCompetition.Services.Data.CommentServices;
    using EssayCompetition.Services.Data.Tests.Common;
    using EssayCompetition.Services.Mapping;
    using EssayCompetition.Web.ViewModels.Essays;
    using Xunit;

    public class CommentServiceTests
    {
        private Seeder seeder;

        public CommentServiceTests()
        {
            this.seeder = new Seeder();
            AutoMapperConfig.RegisterMappings(
                typeof(Comment).GetTypeInfo().Assembly,
                typeof(CommentViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task GetCommentsFromEssayTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var comment = await this.seeder.AddCommentToEssay(context, "testComment", essay.Id);
            var commentRepository = new EfDeletableEntityRepository<Comment>(context);
            var service = new CommentService(commentRepository);

            var resultedComment = service.GetCommentsFromEssay<CommentViewModel>(comment.EssayId);

            Assert.True(resultedComment.First().Content == comment.Content, "GetEssayDetails method does not work correctly");
        }

        [Fact]
        public async Task GetCommentsCountTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var comment = await this.seeder.AddCommentToEssay(context, "testComment", essay.Id);
            var comment2 = await this.seeder.AddCommentToEssay(context, "testComment2", essay.Id);
            var commentRepository = new EfDeletableEntityRepository<Comment>(context);
            var service = new CommentService(commentRepository);

            var expectedCount = context.Comments.Where(x => x.EssayId == essay.Id).Count();
            var resultedCount = service.GetCommentsCount(essay.Id);

            Assert.True(resultedCount == expectedCount, "GetCommentsCount method does not work correctly");
        }

        [Fact]
        public async Task AddCommentAsyncTest()
        {
            var context = EssayCompetitionContextInMemoryFactory.InitializeContext();
            var essay = await this.seeder.SeedEssayAsync(context);
            var commentContent = "testComment";
            var user = await this.seeder.SeedUserAsync(context, "test@abv.bg");
            var commentRepository = new EfDeletableEntityRepository<Comment>(context);
            var service = new CommentService(commentRepository);

            await service.AddCommentAsync(user.Id, essay.Id, commentContent);
            var result = service.GetCommentsFromEssay<CommentViewModel>(essay.Id);

            Assert.True(result.Any(x => x.Content == commentContent) == true, "AddCommentAsync method does not work correctly");
        }
    }
}
