using AutoMapper;
using Ninject;
using Ninject.Modules;
using VehicleStuffDemo.Controllers;

namespace VehicleStuffDemo.App_Start
{
    public class MapperModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));

            Bind<VehicleMakeController>().ToSelf().InTransientScope();
            Bind<VehicleModelController>().ToSelf().InTransientScope();
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            return config;
        }
    }
}