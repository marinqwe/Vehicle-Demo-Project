using System.Collections.Generic;

namespace VehicleDataAccess.Interfaces
{
    internal interface IVehicleMake
    {
        int MakeId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        ICollection<VehicleModel> VehicleModels { get; set; }
    }
}