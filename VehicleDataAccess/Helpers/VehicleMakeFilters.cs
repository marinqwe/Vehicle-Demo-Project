using System;

namespace VehicleDataAccess.Helpers
{
    public class VehicleMakeFilters
    {
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }

        public VehicleMakeFilters(string searchString, string currentFilter)
        {
            CurrentFilter = currentFilter;
            SearchString = searchString;
        }

        public bool ShouldFilterMakes()
        {
            if (!String.IsNullOrEmpty(SearchString))
            {
                return true;
            } 
            return false;
        }
    }
}