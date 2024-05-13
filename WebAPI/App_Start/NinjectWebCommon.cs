[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebAPI.App_Start.NinjectWebCommon), "Stop")]

namespace WebAPI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Voting.Data.Contracts;
    using Voting.Data.Model;
    using Voting.Data.Services;
    using Voting.Domain.CommandHandler;
    using Voting.Domain.Contracts;
    using Voting.Domain.Model;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

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

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IEntityService<VotersDetails>>().To<VotersService>();
            kernel.Bind<IJsonFileDataRepository<Voters>>().To<VotersFileData>();


            kernel.Bind<IEntityService<CandidatesDetails>>().To<CandidatesService>();
            kernel.Bind<IJsonFileDataRepository<Candidates>>().To<CandidatesFileData>();


            kernel.Bind<IVotersStandaloneServices>().To<VotersService>();
            kernel.Bind<ICandidateStandaloneServices>().To<CandidatesService>();

            kernel.Bind<IStandaloneFileDataRepository>().To<StandaloneFileDataRepository>();

        }
    }
}