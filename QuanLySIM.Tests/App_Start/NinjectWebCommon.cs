[assembly: WebActivator.PreApplicationStartMethod(typeof(QuanLySIM.Tests.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(QuanLySIM.Tests.App_Start.NinjectWebCommon), "Stop")]

namespace QuanLySIM.Tests.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using QuanLySIM.Data.DbInteractions;
    using QuanLySIM.Data.EntityRepositories;
    using QuanLySIM.Tests.FakeRepositories;
    using QuanLySIM.Tests.FakeDataIneractions;
    using QuanLySIM.Tests.FakeServices;
    using QuanLySIM.Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            kernel.Bind<IDbFactory>().To<FakeDbFactory>();

            kernel.Bind<IDbService>().To<DbService>();

            kernel.Bind<ICustomerRepository>().To<FakeCustomerRepository>();

            kernel.Bind<IGroupRepository>().To<FakeGroupRepository>();

            kernel.Bind<IOrderRepository>().To<FakeOrderRepository>();

            kernel.Bind<IRoleRepository>().To<FakeRoleRepository>();

            kernel.Bind<IStaffRepository>().To<FakeStaffRepository>();

            kernel.Bind<ISimRepository>().To<FakeSimRepository>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
