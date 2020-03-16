using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDataAccess.Implementations;

namespace VehicleDataAccess
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IValidationDictionary _validationDictionary;
        private readonly IVehicleMakeRepository _vehicleRepository;

        public VehicleMakeService()
        {
        }

        public VehicleMakeService(IValidationDictionary validationDictionary, IVehicleMakeRepository vehicleRepository)
        {
            _validationDictionary = validationDictionary;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<VehicleMake>> GetVehicleMakeListAsync()
        {
            return await _vehicleRepository.GetVehicleMakeList();
        }

        protected bool ValidateVehicleMake(VehicleMake vehicleMake)
        {
            if (vehicleMake.Name == null)
            {
                _validationDictionary.AddError("Name", "Name is required.");
            }
            if (vehicleMake.Abrv == null)
            {
                _validationDictionary.AddError("Abrv", "Abrv is required.");
            }
            return _validationDictionary.IsValid;
        }

        public async Task<VehicleMake> FindVehicleMakeAsync(int? id)
        {
            return await _vehicleRepository.FindVehicleMake(id);
        }

        public async Task<bool> CreateVehicle(VehicleMake vehicleMake)
        {
            if (!ValidateVehicleMake(vehicleMake))
                return false;
            bool res = await _vehicleRepository.CreateVehicle(vehicleMake);
            return res;
        }

        public async Task<bool> DeleteVehicle(VehicleMake vehicleMake)
        {
            bool res = await _vehicleRepository.DeleteVehicle(vehicleMake);
            return res;
        }

        public async Task<bool> EditVehicle()
        {
            bool res = await _vehicleRepository.EditVehicle();
            return res;
        }
    }
}