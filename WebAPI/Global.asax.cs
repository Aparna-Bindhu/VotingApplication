using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI.App_Start;

namespace WebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //NinjectWebCommon.Start();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(NinjectWebCommon.CreateKernel());
        }
    }
}
