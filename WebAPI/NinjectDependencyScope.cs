using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace WebAPI
{
    public class NinjectDependencyScope : IDependencyScope
    {
        protected IResolutionRoot resolutionRoot;

        public NinjectDependencyScope(IResolutionRoot kernel)
        {
            resolutionRoot = kernel;
        }

        public object GetService(Type serviceType)
        {
            return resolutionRoot.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return resolutionRoot.GetAll(serviceType);
        }

        public void Dispose()
        {
            IDisposable disposable = (IDisposable)resolutionRoot;
            if (disposable != null)
                disposable.Dispose();
            resolutionRoot = null;
        }
    }
}