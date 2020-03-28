using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDataAccess.Helpers;
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

        public async Task<IEnumerable<VehicleMake>> GetVehicleMakeListAsync(VehicleMakeFilters filters, VehicleMakeSorting sorting, VehicleMakePaging paging)
        {
            var vehicles = from vehicle in await _vehicleRepository.GetVehicleMakeList()
                           select vehicle;

            paging.TotalCount = vehicles.Count();

            //filter/find
            if (filters.ShouldFilterMakes())
            {
                vehicles = vehicles.Where(m => m.Name.Contains(filters.SearchString) || m.Abrv.Contains(filters.SearchString));
            }
            
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
            return vehicles.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage).ToList();
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