using AspNetSearch.Models.Search;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AspNetSearch.App_Start
{
    public class SimpleInjectorConfig
    {

        public static void RegisterConfig()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            container.Register<IFetchTableInfoRepository, FetchTableInfoRepositoryInMemory>(Lifestyle.Scoped);
            container.Register<ISaveSearchSettingRepository, SaveSearchSettingRepositoryInMemory>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

    }
}