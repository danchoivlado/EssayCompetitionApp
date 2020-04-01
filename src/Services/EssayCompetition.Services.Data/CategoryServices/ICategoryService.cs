namespace EssayCompetition.Services.Data.CategoryServices
{
    using System.Collections;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        IEnumerable<T> GetAll<T>(int currentPage, int pageSize);

        IEnumerable<T> GetAll<T>();

        int GetCount();
    }
}
