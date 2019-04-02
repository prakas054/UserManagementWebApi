﻿using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using UserManagement.Controllers;
using UserManagement.Models;
using UserManagement.Repository;

namespace UserManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            // Get your HttpConfiguration.
            var configs = GlobalConfiguration.Configuration;

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UserRepository>().As<IUserRepository<Users>>().SingleInstance();
            builder.RegisterType<UserRepository>().As<ISearchUser>().SingleInstance();
            builder.RegisterType<UsersController>().SingleInstance();
            builder.RegisterType<Users>().SingleInstance();

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(configs);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

          // config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
