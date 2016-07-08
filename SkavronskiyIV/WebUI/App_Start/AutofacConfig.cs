using Autofac;
using Autofac.Integration.Mvc;
//using Autofac.Integration.Owin;
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
using Repository;

namespace WebUI
{
    public class AutofacConfig
    {

        // метод для конфигурации MVC контроллера
        public static void Configure(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // для webApi
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // для MVC
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            #region регистрация репозиториев

            builder.RegisterType<ResumeRepository>().As<IResumeRepository>();
            // и так для всех репозиториев

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