using Ninject.Modules;
using VehicleDataAccess.Implementations;

namespace VehicleDataAccess
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