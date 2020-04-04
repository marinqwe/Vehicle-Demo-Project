using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleDataAccess.Helpers;
using VehicleDataAccess.Implementations;

namespace VehicleDataAccess
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly VehicleContext _entities = new VehicleContext();

        public async Task<IEnumerable<VehicleModel>> GetVehicleModelList(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            IQueryable<VehicleModel> models = from model in _entities.VehicleModels
                                              select model;

            if (filters.ShouldApplyFilters())
            {
                models = models.Where(m => m.Name.Contains(filters.FilterBy)
                                    || m.Abrv.Contains(filters.FilterBy)
                                    || m.MakeId.ToString().Contains(filters.FilterBy));
            }

            paging.TotalCount = models.Count();
            // sort
            switch (sorting.SortBy)
            {
                case "name_desc":
                    models = models.OrderByDescending(v => v.Name);
                    break;

                case "Abrv":
                    models = models.OrderBy(v => v.Abrv);
                    break;

                case "abrv_desc":
                    models = models.OrderByDescending(v => v.Abrv);
                    break;

                case "MakeId":
                    models = models.OrderBy(v => v.MakeId);
                    break;

                case "makeid_desc":
                    models = models.OrderByDescending(v => v.MakeId);
                    break;

                default: // sort by name
                    models = models.OrderBy(v => v.Name);
                    break;
            }
            return await models.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage).ToListAsync();
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