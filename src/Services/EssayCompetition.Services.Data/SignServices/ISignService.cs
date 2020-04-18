using System.Threading.Tasks;

namespace EssayCompetition.Services.Data.SignServices
{
    public interface ISignService
    {
        bool UserAlreadyRegisteredForCompetition(string userId, int contestId);

        int GetNextContestId();

        string GetContestName(int id);

        Task RegisterForContestAsync(string userId, int contestId);
    }
}
