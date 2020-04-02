using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleDataAccess.Helpers;
using VehicleDataAccess.Implementations;

namespace VehicleDataAccess
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly VehicleContext _entities = new VehicleContext();

        // Vehicle Make
        public async Task<IEnumerable<VehicleMake>> GetVehicleMakeList(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            var vehicles = from vehicle in _entities.VehicleMakes
                           select vehicle;

            //filter/find
            if (filters.ShouldApplyFilters())
            {
                vehicles = vehicles.Where(m => m.Name.Contains(filters.FilterBy) || m.Abrv.Contains(filters.FilterBy));
            }

            paging.TotalCount = vehicles.Count();
            // sort
            switch (sorting.SortBy)
            {
                case "name_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Name);
                    break;

                case "Abrv":
                    vehicles = vehicles.OrderBy(v => v.Abrv);
                    break;

                case "abrv_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Abrv);
                    break;

                default: // sort by name
                    vehicles = vehicles.OrderBy(v => v.Name);
                    break;
            }

            return await vehicles.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage).ToListAsync();
        }

        public async Task<VehicleMake> FindVehicleMake(int? id)
        {
            _entities.Database.Log = Console.Write;
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