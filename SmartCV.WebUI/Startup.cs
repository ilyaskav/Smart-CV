using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartCV.Entity.Classes;
using SmartCV.Service.Classes;
using SmartCV.Service.Interfaces;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System;
using SmartCV.Repository.Classes;
using SmartCV.Repository.Interfaces;

namespace SmartCV.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<Entity.ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole<long>>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<Entity.ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // create new container builder
            var containerBuilder = new ContainerBuilder();
            // populate .NET Core services
            containerBuilder.Populate(services);
            
            // register your autofac modules           
            #region регистрация сервисов

            // TODO
            containerBuilder.RegisterType<ResumeService>().As<IResumeService>();
            containerBuilder.RegisterType<ResumeManagerService>().As<IResumeManagerService>();
            containerBuilder.RegisterType<ProfessionService>().As<IProfessionService>();
            containerBuilder.RegisterType<ContactService>().As<IContactService>();
            containerBuilder.RegisterType<InstitutionService>().As<IInstitutionService>();
            containerBuilder.RegisterType<WorkPlaceService>().As<IWorkPlaceService>();
            containerBuilder.RegisterType<CertificateService>().As<ICertificateService>();
            containerBuilder.RegisterType<SkillService>().As<ISkillService>();
            containerBuilder.RegisterType<LanguageService>().As<ILanguageService>();

            #endregion

            #region регистрация репозиториев

            containerBuilder.RegisterType<PersonalDataRepository>().As<IPersonalDataRepository>();
            containerBuilder.RegisterType<CertificateRepository>().As<ICertificateRepository>();
            containerBuilder.RegisterType<ContactRepository>().As<IContactRepository>();
            containerBuilder.RegisterType<ContactTitleRepository>().As<IContactTitleRepository>();
            containerBuilder.RegisterType<DutyRepository>().As<IDutyRepository>();
            containerBuilder.RegisterType<InstitutionRepository>().As<IInstitutionRepository>();
            containerBuilder.RegisterType<LanguageRepository>().As<ILanguageRepository>();
            containerBuilder.RegisterType<PersonalQualityRepository>().As<IPersonalQualityRepository>();
            containerBuilder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            containerBuilder.RegisterType<SkillRepository>().As<ISkillRepository>();
            containerBuilder.RegisterType<WorkPlaceRepository>().As<IWorkPlaceRepository>();
            containerBuilder.RegisterType<ResumeRepository>().As<IResumeRepository>();
            containerBuilder.RegisterType<ProfessionRepository>().As<IProfessionRepository>();

            #endregion



            // build container
            var container = containerBuilder.Build();

            // return service provider
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
