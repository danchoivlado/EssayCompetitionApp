namespace EssayCompetition.Services.Data.UsersServices
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        int GetUsersCount();

        IEnumerable<T> GetUsersWithRoles<T>();

        IEnumerable<T> GetUsersWithRoles<T>(int currentPage, int pageSize);

        IEnumerable<string> GetUserRolesNames(IEnumerable<string> rolesIds);

        bool HasUserWithId(string id);

        T GetUserById<T>(string id);
    }
}
