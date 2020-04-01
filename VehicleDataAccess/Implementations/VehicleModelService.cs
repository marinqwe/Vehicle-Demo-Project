using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDataAccess.Helpers;
using VehicleDataAccess.Implementations;

namespace VehicleDataAccess
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IVehicleModelRepository _vehicleRepository;

        public VehicleModelService()
        {
        }

        public VehicleModelService(IValidationDictionary validationDictionary, IVehicleModelRepository vehicleRepository)
        {
            _validationDictionary = validationDictionary;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<VehicleModel>> GetVehicleModelList(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            return await _vehicleRepository.GetVehicleModelList(filters, sorting, paging);
        }

        protected bool ValidateVehicleModel(VehicleModel vehicleModel)
        {
            if (vehicleModel.Name == null)
            {
                _validationDictionary.AddError("Name", "Name is required.");
            }
            if (vehicleModel.Abrv == null)
            {
                _validationDictionary.AddError("Abrv", "Abrv is required.");
            }
            return _validationDictionary.IsValid;
        }

        public async Task<VehicleModel> FindVehicleModel(int? id)
        {
            return await _vehicleRepository.FindVehicleModel(id);
        }

        public async Task<bool> CreateVehicleModel(VehicleModel vehicleModel)
        {
            if (!ValidateVehicleModel(vehicleModel))
                return false;
            bool res = await _vehicleRepository.CreateVehicleModel(vehicleModel);
            return res;
        }

        public async Task<bool> DeleteVehicleModel(VehicleModel vehicleModel)
        {
            bool res = await _vehicleRepository.DeleteVehicleModel(vehicleModel);
            return res;
        }

        public async Task<bool> EditVehicleModel()
        {
            bool res = await _vehicleRepository.EditVehicleModel();
            return res;
        }
    }
}