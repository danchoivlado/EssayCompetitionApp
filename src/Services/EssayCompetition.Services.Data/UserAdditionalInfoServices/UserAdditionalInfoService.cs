namespace EssayCompetition.Services.Data.UserAdditionalInfoServices
{
    using System.Linq;
    using System.Threading.Tasks;
    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class UserAdditionalInfoService : IUserAdditionalInfoService
    {
        private readonly IDeletableEntityRepository<UserAdditionalInfo> userAdditionalInfoRepository;

        public UserAdditionalInfoService(IDeletableEntityRepository<UserAdditionalInfo> userAdditionalInfoRepository)
        {
            this.userAdditionalInfoRepository = userAdditionalInfoRepository;
        }

        public T GetUserWithIdAdditionalInfo<T>(string userId)
        {
            return this.userAdditionalInfoRepository.All().Where(x => x.UserId == userId).To<T>().First();
        }

        public bool HasUserAdditionalInfoWithId(string userId)
        {
            return this.userAdditionalInfoRepository.All().Any(x => x.UserId == userId);
        }

        public async Task UpdateUserAdditionalInfoAsync(
            string userId,
            string fullName,
            string imageUrl,
            string contactPhone,
            string contactEmail,
            string country,
            string city,
            string social)
        {
            var userAdditionalInfo = new UserAdditionalInfo()
            {
                UserId = userId,
                FullName = fullName,
                ImageUrl = imageUrl,
                ConntactPhone = contactPhone,
                ContactEmail = contactEmail,
                Country = country,
                City = city,
                Social = social,
            };

            if (this.HasUserAdditionalInfoWithId(userId))
            {
                userAdditionalInfo.Id = this.GetUserAdditionalInfoIdWithUserId(userId);
                this.userAdditionalInfoRepository.Update(userAdditionalInfo);
            }
            else
            {
                await this.userAdditionalInfoRepository.AddAsync(userAdditionalInfo);
            }

            await this.userAdditionalInfoRepository.SaveChangesAsync();
        }

        private int GetUserAdditionalInfoIdWithUserId(string userId)
        {
            return this.userAdditionalInfoRepository.AllAsNoTracking().First(x => x.UserId == userId).Id;
        }
    }
}
