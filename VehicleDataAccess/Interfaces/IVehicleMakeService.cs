using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDataAccess.Helpers;

namespace VehicleDataAccess.Implementations
{
    public interface IVehicleMakeService
    {
        Task<IEnumerable<VehicleMake>> GetVehicleMakeListAsync(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);

        Task<VehicleMake> FindVehicleMakeAsync(int? id);

        Task<bool> CreateVehicle(VehicleMake vehicleMake);

        Task<bool> DeleteVehicle(VehicleMake vehicleMake);

        Task<bool> EditVehicle();
    }
}