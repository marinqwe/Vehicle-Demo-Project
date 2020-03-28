using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<VehicleModel>> GetVehicleModelList(VehicleModelFilters filters, VehicleModelSorting sorting, VehicleModelPaging paging)
        {
            var models = from model in await _vehicleRepository.GetVehicleModelList()
                         select model;
            paging.TotalCount = models.Count();

            if (filters.ShouldFilterModels())
            {
                models = models.Where(m => m.Name.Contains(filters.SearchString)
                                    || m.Abrv.Contains(filters.SearchString)
                                    || m.MakeId.ToString().Contains(filters.SearchString));
            }
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

            return models.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage).ToList();
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