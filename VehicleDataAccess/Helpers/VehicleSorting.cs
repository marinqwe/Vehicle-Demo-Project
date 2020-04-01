using System;

namespace VehicleDataAccess.Helpers
{
    public class VehicleSorting
    {
        public string SortBy { get; set; }
        public string SortByName { get; set; }
        public string SortByAbrv { get; set; }
        public string OrderVehiclesBy { get; set; }

        public VehicleSorting(string sortBy)
        {
            SortBy = sortBy;
            SortByName = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            SortByAbrv = sortBy == "Abrv" ? "abrv_desc" : "Abrv";
        }
    }
}