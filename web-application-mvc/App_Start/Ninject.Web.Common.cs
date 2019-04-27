[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(web_application_mvc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(web_application_mvc.App_Start.NinjectWebCommon), "Stop")]

namespace web_application_mvc.App_Start
{
    using global::Ninject;
    using global::Ninject.Modules;
    using global::Ninject.Web.Common;
    using global::Ninject.Web.Common.WebHost;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Services.Business;
    using System;
    using System.Web;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var modules = new INinjectModule[] { new ServiceModule("TestContext") };
            var kernel = new StandardKernel(modules);
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new Ninject.NinjectDependencyResolver(kernel));
        }        
    }
}