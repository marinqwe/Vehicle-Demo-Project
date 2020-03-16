using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleDataAccess.Implementations
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetVehicleModelList();

        Task<VehicleModel> FindVehicleModel(int? id);

        Task<bool> CreateVehicleModel(VehicleModel vehicleModel);

        Task<bool> DeleteVehicleModel(VehicleModel vehicleModel);

        Task<bool> EditVehicleModel();
    }
}