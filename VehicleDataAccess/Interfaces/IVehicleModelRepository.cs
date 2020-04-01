using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDataAccess.Helpers;

namespace VehicleDataAccess.Implementations
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetVehicleModelList(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);

        Task<VehicleModel> FindVehicleModel(int? id);

        Task<bool> CreateVehicleModel(VehicleModel vehicleModel);

        Task<bool> DeleteVehicleModel(VehicleModel vehicleModel);

        Task<bool> EditVehicleModel();
    }
}