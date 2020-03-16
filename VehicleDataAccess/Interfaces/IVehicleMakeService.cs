using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleDataAccess.Implementations
{
    public interface IVehicleMakeService
    {
        Task<IEnumerable<VehicleMake>> GetVehicleMakeListAsync();

        Task<VehicleMake> FindVehicleMakeAsync(int? id);

        Task<bool> CreateVehicle(VehicleMake vehicleMake);

        Task<bool> DeleteVehicle(VehicleMake vehicleMake);

        Task<bool> EditVehicle();
    }
}