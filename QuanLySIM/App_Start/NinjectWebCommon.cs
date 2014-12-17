[assembly: WebActivator.PreApplicationStartMethod(typeof(QuanLySIM.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(QuanLySIM.App_Start.NinjectWebCommon), "Stop")]

namespace QuanLySIM.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using QuanLySIM.Data.DbInteractions;
    using QuanLySIM.Data.EntityRepositories;
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

            kernel.Bind<IDbFactory>().To<DbFactory>();

            kernel.Bind<IDbService>().To<DbService>();

            kernel.Bind<ICustomerRepository>().To<CustomerRepository>();
            kernel.Bind<ICustomerService>().To<CustomerService>();

            kernel.Bind<IGroupRepository>().To<GroupRepository>();
            kernel.Bind<IGroupService>().To<GroupService>();

            kernel.Bind<IOrderRepository>().To<OrderRepository>();
            kernel.Bind<IOrderService>().To<OrderService>();

            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IRoleService>().To<RoleService>();

            kernel.Bind<IStaffRepository>().To<StaffRepository>();
            kernel.Bind<IStaffService>().To<StaffService>();

            kernel.Bind<ISimRepository>().To<SimRepository>();
            kernel.Bind<ISimService>().To<SimService>();
            
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
