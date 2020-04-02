namespace EssayCompetition.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data;
    using EssayCompetition.Web.ViewModels.Settings;

    using Microsoft.AspNetCore.Mvc;

    public class SettingsController : BaseController
    {
        private readonly ISettingsService settingsService;

        private readonly IDeletableEntityRepository<Setting> repository;

        public SettingsController(ISettingsService settingsService, IDeletableEntityRepository<Setting> repository)
        {
            this.settingsService = settingsService;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var settings = this.settingsService.GetAll<SettingViewModel>();
            var model = new SettingsListViewModel { Settings = settings };
            return this.View(model);
        }

        public async Task<IActionResult> InsertSetting()
        {
            var random = new Random();
            var setting = new Setting { Name = $"Name_{random.Next()}", Value = $"Value_{random.Next()}" };

            await this.repository.AddAsync(setting);
            await this.repository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
