using Ninject.Modules;
using VehicleDataAccess;
using VehicleDataAccess.Implementations;

namespace VehicleStuffDemo.App_Start
{
    public class VehicleModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleMakeService>().To<VehicleMakeService>();
            Bind<IVehicleModelService>().To<VehicleModelService>();
            Bind<IValidationDictionary>().To<ModelStateWrapper>();
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
        }
    }
}