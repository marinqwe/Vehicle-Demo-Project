﻿using System;
using VehicleDataAccess.Interfaces;

namespace VehicleDataAccess.Helpers
{
    public class VehicleSorting : IVehicleSorting
    {
        public string SortBy { get; set; }
        public string SortByName { get; set; }
        public string SortByAbrv { get; set; }
        public string SortById { get; set; }

        public VehicleSorting(string sortBy)
        {
            SortBy = sortBy;
            SortByName = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            SortByAbrv = sortBy == "Abrv" ? "abrv_desc" : "Abrv";
            SortById = sortBy == "MakeId" ? "makeid_desc" : "MakeId";
        }
    }
}