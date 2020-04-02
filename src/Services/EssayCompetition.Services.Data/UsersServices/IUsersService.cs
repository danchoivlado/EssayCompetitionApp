namespace EssayCompetition.Services.Data.UsersServices
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<T> GetUsersWithRoles<T>();

        IEnumerable<string> GetUserRolesNames(IEnumerable<string> rolesIds);
    }
}
