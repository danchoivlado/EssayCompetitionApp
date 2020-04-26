namespace EssayCompetition.Services.Data.CategoryServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        IEnumerable<T> GetAll<T>(int currentPage, int pageSize);

        IEnumerable<T> GetAll<T>();

        int GetCount();

        Task CreateAsync(string title, string description, string imageUrl);

        Task DeleteAsync(int id);

        Task<bool> HasWithIdAsync(int id);

        T GetWithId<T>(int id);

        Task UpdateAsync(int id, string title, string description, string imageUrl);

        int GetFirstOrDefaultCategoryId();
    }
}
