namespace EssayCompetition.Web
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;
    using EssayCompetition.Data;
    using EssayCompetition.Data.Common;
    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Data.Repositories;
    using EssayCompetition.Data.Seeding;
    using EssayCompetition.Services.Data;
    using EssayCompetition.Services.Data.CalendarServices;
    using EssayCompetition.Services.Data.CategoryServices;
    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.ImageServices;
    using EssayCompetition.Services.Data.RolesServices;
    using EssayCompetition.Services.Data.SignServices;
    using EssayCompetition.Services.Data.TeacherReviewedServices;
    using EssayCompetition.Services.Data.TeacherServices;
    using EssayCompetition.Services.Data.UsersServices;
    using EssayCompetition.Services.Mapping;
    using EssayCompetition.Services.Messaging;
    using EssayCompetition.Web.ViewModels;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            Account account = new Account(
                this.configuration["CloudinarySettings:CloudName"],
                this.configuration["CloudinarySettings:ApiKey"],
                this.configuration["CloudinarySettings:ApiSecret"]);

            services.AddRecaptcha(new RecaptchaOptions
            {
                SiteKey = this.configuration["Recaptcha:SiteKey"],
                SecretKey = this.configuration["Recaptcha:SecretKey"],
                ValidationMessage = "Are you a robot?",
            });

            Cloudinary cloudinary = new Cloudinary(account);
            HtmlSanitizer htmlSanitizer = new HtmlSanitizer();
            services.AddSingleton(cloudinary);
            services.AddSingleton(htmlSanitizer);

            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ITeacherReviewedService, TeacherReviewedService>();
            services.AddTransient<IContestService, ContestService>();
            services.AddTransient<ICalendarService, CalendarService>();
            services.AddTransient<ISignService, SignService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
