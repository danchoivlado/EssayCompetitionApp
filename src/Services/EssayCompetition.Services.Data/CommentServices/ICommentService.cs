namespace EssayCompetition.Services.Data.CommentServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        IEnumerable<T> GetCommentsFromEssay<T>(int essayId);

        Task AddCommentAsync(string userId, int essayId, string commentContent);

        int GetCommentsCount(int essayId);
    }
}
