﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDataAccess.Helpers;

namespace VehicleDataAccess.Implementations
{
    public interface IVehicleMakeRepository
    {
        Task<IEnumerable<VehicleMake>> GetVehicleMakeList(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);

        Task<VehicleMake> FindVehicleMake(int? id);

        Task<bool> CreateVehicle(VehicleMake vehicleMake);

        Task<bool> DeleteVehicle(VehicleMake vehicleMake);

        Task<bool> EditVehicle();
    }
}