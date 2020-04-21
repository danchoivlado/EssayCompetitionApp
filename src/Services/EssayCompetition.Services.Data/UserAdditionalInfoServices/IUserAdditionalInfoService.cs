namespace EssayCompetition.Services.Data.UserAdditionalInfoServices
{
    using System.Threading.Tasks;

    public interface IUserAdditionalInfoService
    {
        T GetUserWithIdAdditionalInfo<T>(string userId);

        bool HasUserAdditionalInfoWithId(string userId);

        Task UpdateUserAdditionalInfoAsync(
            string userId,
            string fullName,
            string imageUrl,
            string contactPhone,
            string contactEmail,
            string country,
            string city,
            string social);
    }
}
