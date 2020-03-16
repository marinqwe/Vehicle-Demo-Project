using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using VehicleDataAccess.Implementations;

namespace VehicleDataAccess
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly VehicleContext _entities = new VehicleContext();

        // Vehicle Make
        public async Task<IEnumerable<VehicleMake>> GetVehicleMakeList()
        {
            return await _entities.VehicleMakes.ToListAsync();
        }

        public async Task<VehicleMake> FindVehicleMake(int? id)
        {
            return await _entities.VehicleMakes.FindAsync(id);
        }

        public async Task<bool> CreateVehicle(VehicleMake vehicleMake)
        {
            try
            {
                _entities.VehicleMakes.Add(vehicleMake);
                await _entities.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteVehicle(VehicleMake vehicleMake)
        {
            try
            {
                _entities.VehicleMakes.Remove(vehicleMake);
                await _entities.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditVehicle()
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