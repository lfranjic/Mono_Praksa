using Autofac;
using Autofac.Integration.WebApi;
using Book.Repository;
using Book.Repository.Common;
using Book.Service;
using Book.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Book.WebAPI.Configuration
{
    public class AutofacConfig : Autofac.Module
    {
        public static void SetupBuilder()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterModule(new BookServiceModule());
            builder.RegisterModule(new BookRepositoryModule());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}