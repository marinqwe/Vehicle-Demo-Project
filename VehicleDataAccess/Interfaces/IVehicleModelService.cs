﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDataAccess.Helpers;

namespace VehicleDataAccess.Implementations
{
    public interface IVehicleModelService
    {
        Task<IEnumerable<VehicleModel>> GetVehicleModelList(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);

        Task<bool> CreateVehicleModel(VehicleModel vehicleModel);

        Task<VehicleModel> FindVehicleModel(int? id);

        Task<bool> DeleteVehicleModel(VehicleModel vehicleModel);

        Task<bool> EditVehicleModel();
    }
}