using System;

namespace VehicleDataAccess.Helpers
{
    public class VehicleModelFilters
    {
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }

        public VehicleModelFilters(string searchString, string currentFilter)
        {
            SearchString = searchString;
            CurrentFilter = currentFilter;
        }

        public bool ShouldFilterModels()
        {
            if (!String.IsNullOrEmpty(SearchString)) return true;
            //if (!String.IsNullOrEmpty(CurrentFilter)) return true;
            return false;
        }
    }
}