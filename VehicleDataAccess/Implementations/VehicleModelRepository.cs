using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using VehicleDataAccess.Implementations;

namespace VehicleDataAccess
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly VehicleContext _entities = new VehicleContext();

        public async Task<IEnumerable<VehicleModel>> GetVehicleModelList()
        {
            return await _entities.VehicleModels.ToListAsync();
        }

        public async Task<VehicleModel> FindVehicleModel(int? id)
        {
            return await _entities.VehicleModels.FindAsync(id);
        }

        public async Task<bool> CreateVehicleModel(VehicleModel vehicleModel)
        {
            try
            {
                _entities.VehicleModels.Add(vehicleModel);
                await _entities.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteVehicleModel(VehicleModel vehicleModel)
        {
            try
            {
                _entities.VehicleModels.Remove(vehicleModel);
                await _entities.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditVehicleModel()
        {
            try
            {
                await _entities.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}