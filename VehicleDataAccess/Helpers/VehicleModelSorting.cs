﻿using System;

namespace VehicleDataAccess.Helpers
{
    public class VehicleModelSorting
    {
        public string SortBy { get; set; }
        public string SortByName { get; set; }
        public string SortByAbrv { get; set; }
        public string SortById { get; set; }

        public VehicleModelSorting(string sortBy)
        {
            SortBy = sortBy;
            SortByName = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            SortByAbrv = sortBy == "Abrv" ? "abrv_desc" : "Abrv";
            SortById = sortBy == "MakeId" ? "makeid_desc" : "MakeId";
        }
    }
}