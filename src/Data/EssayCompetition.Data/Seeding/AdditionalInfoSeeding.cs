using EssayCompetition.Common;
using EssayCompetition.Data.Models;
using EssayCompetition.Services.Data.UserAdditionalInfoServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EssayCompetition.Data.Seeding
{
    public class AdditionalInfoSeeding : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userAdditionalInfoService = serviceProvider.GetRequiredService<IUserAdditionalInfoService>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var usersAdditionalInfoList = new List<(string ImageUrl, string FullName, string ContactEmail, string ContactPhone, string Country, string City, string Social)>()
            {
                ("https://res.cloudinary.com/dzqsypfen/image/upload/v1587796437/8_Useful_Travel_Photography_Tips_For_Improving_Your_Photos_xwzihp.jpg", "Elizabed1", "Elizabed1@abv.bg", "0111111111", "Bulgaria1", "Sliven1", "Instagram - Elizabed1"),
                ("https://res.cloudinary.com/dzqsypfen/image/upload/v1587796417/These_beauty_tips_will_have_you_looking_gorgeous_--_no_filter_required_kzfakr.jpg", "Elizabed2", "Elizabed2@abv.bg", "0222222222", "Bulgaria2", "Sliven2", "Instagram - Elizabed2"),
                ("https://res.cloudinary.com/dzqsypfen/image/upload/v1587796263/Whatsapp_DP_Images_For_Girl_r1wnbj.jpg", "Elizabed3", "Elizabed3@abv.bg", "0333333333", "Bulgaria3", "Sliven3", "Instagram - Elizabed3"),
            };

            List<string> contestarsNameList = new List<string>()
            {
                GlobalSeedDataConstants.Contestant1Email,
                GlobalSeedDataConstants.Contestant2Email,
                GlobalSeedDataConstants.Contestant3Email,
            };

            await SeedAdditionalInfoAsync(contestarsNameList, userManager, userAdditionalInfoService, usersAdditionalInfoList);
        }

        private static async Task SeedAdditionalInfoAsync(List<string> contestarsNameList, UserManager<ApplicationUser> userManager, IUserAdditionalInfoService userAdditionalInfoService, List<(string ImageUrl, string FullName, string ContactEmail, string ContactPhone, string Country, string City, string Social)> usersAdditionalInfoList)
        {
            int counter = 0;
            foreach (var userName in contestarsNameList)
            {
                var user = await userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    throw new NullReferenceException($"No user with {userName} sedded");
                }

                if (!userAdditionalInfoService.HasUserAdditionalInfoWithId(user.Id))
                {
                    var currentInfo = usersAdditionalInfoList[counter];
                    await userAdditionalInfoService.UpdateUserAdditionalInfoAsync(
                        user.Id,
                        currentInfo.FullName,
                        currentInfo.ImageUrl,
                        currentInfo.ContactPhone,
                        currentInfo.ContactEmail,
                        currentInfo.Country,
                        currentInfo.City,
                        currentInfo.Social);
                }

                counter++;
            }
        }
    }
}
