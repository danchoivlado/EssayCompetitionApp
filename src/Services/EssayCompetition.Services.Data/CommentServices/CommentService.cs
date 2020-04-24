namespace EssayCompetition.Services.Data.CommentServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public CommentService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task AddCommentAsync(string userId, int essayId, string commentContent)
        {
            var comment = new Comment()
            {
                Content = commentContent,
                EssayId = essayId,
                UserId = userId,
            };

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

        public int GetCommentsCount(int essayId)
        {
            return this.commentRepository.All().Where(x => x.EssayId == essayId).Count();
        }

        public IEnumerable<T> GetCommentsFromEssay<T>(int essayId)
        {
            return this.commentRepository.All().Where(x => x.EssayId == essayId).To<T>();
        }
    }
}
