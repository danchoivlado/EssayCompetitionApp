﻿namespace EssayCompetition.Services.Data.SignServices
{
    using System.Threading.Tasks;

    public interface ISignService
    {
        bool UserAlreadyRegisteredForCompetition(string userId, int contestId);

        int GetNextContestId();

        string GetContestName(int id);

        Task RegisterForContestAsync(string userId, int contestId);
    }
}
