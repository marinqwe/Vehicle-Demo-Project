using AutoMapper;
using VehicleDataAccess;
using VehicleStuffDemo.ViewModels;

namespace VehicleStuffDemo.App_Start
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>();
            CreateMap<VehicleModel, VehicleModelViewModel>();
        }
    }
}