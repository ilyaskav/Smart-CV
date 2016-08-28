using Autofac;
using Autofac.Integration.Mvc;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Autofac.Integration.Owin;
using System.Reflection;
using Repository.Classes;
using Repository.Interfaces;
using Services.Classes;
using Services.Interfaces;

namespace WebUI
{
    public class AutofacConfig
    {

        // метод для конфигурации MVC контроллера
        public static void Configure(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            #region регистрация контроллеров
            // для webApi
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // для MVC
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            #endregion

            #region регистрация сервисов

            // TODO
            builder.RegisterType<ResumeService>().As<IResumeService>();
            builder.RegisterType<ResumeManagerService>().As<IResumeManagerService>();
            builder.RegisterType<ProfessionService>().As<IProfessionService>();
            builder.RegisterType<ContactService>().As<IContactService>();
            builder.RegisterType<InstitutionService>().As<IInstitutionService>();

            #endregion

            #region регистрация репозиториев

            builder.RegisterType<ResumeRepository>().As<IResumeRepository>();
            builder.RegisterType<CertificateRepository>().As<ICertificateRepository>();
            builder.RegisterType<ContactRepository>().As<IContactRepository>();
            builder.RegisterType<ContactTitleRepository>().As<IContactTitleRepository>();
            builder.RegisterType<DutyRepository>().As<IDutyRepository>();
            builder.RegisterType<InstitutionRepository>().As<IInstitutionRepository>();
            builder.RegisterType<LanguageRepository>().As<ILanguageRepository>();
            builder.RegisterType<PersonalQualityRepository>().As<IPersonalQualityRepository>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<SkillRepository>().As<ISkillRepository>();
            builder.RegisterType<WorkPlaceRepository>().As<IWorkPlaceRepository>();
            builder.RegisterType<ResumeManagerRepository>().As<IResumeManagerRepository>();
            builder.RegisterType<ProfessionRepository>().As<IProfessionRepository>();

            #endregion

            // регестрируем контекст данных и указываем его процесс создания
            builder.RegisterType<Entities.ApplicationDbContext>().InstancePerRequest();


            var container = builder.Build();
            // метод для конфигурации MVC контроллера
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            // строчка для WebAPI
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

        }
    }
}