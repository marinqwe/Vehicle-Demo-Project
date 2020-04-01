using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VehicleDataAccess;

namespace VehicleStuffDemo.App_Start
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel(new VehicleModule(), new MapperModule());
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
        }
    }
}