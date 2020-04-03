namespace EssayCompetition.Services.Data.UsersServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        int GetUsersCount();

        IEnumerable<T> GetUsersWithRoles<T>();

        IEnumerable<T> GetUsersWithRoles<T>(int currentPage, int pageSize);

        IEnumerable<string> GetUserRolesNames(IEnumerable<string> rolesIds);

        bool HasUserWithId(string id);

        T GetUserById<T>(string id);

        Task UpdateUserAsync(string userId, string userName, string email);

        Task DeleteUserAsync(string userId);
    }
}
