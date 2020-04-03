namespace EssayCompetition.Services.Data.RolesServices
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRolesService
    {
        Task CreateRoleAsync<T>(IQueryable<T> role);
    }
}
