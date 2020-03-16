using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleDataAccess.Implementations
{
    public interface IVehicleModelService
    {
        //asdf
        Task<IEnumerable<VehicleModel>> GetVehicleModelList();

        Task<bool> CreateVehicleModel(VehicleModel vehicleModel);

        Task<VehicleModel> FindVehicleModel(int? id);

        Task<bool> DeleteVehicleModel(VehicleModel vehicleModel);

        Task<bool> EditVehicleModel();
    }
}