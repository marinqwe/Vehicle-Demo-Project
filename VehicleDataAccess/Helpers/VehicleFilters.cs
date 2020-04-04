using VehicleDataAccess.Interfaces;

namespace VehicleDataAccess.Helpers
{
    public class VehicleFilters : IVehicleFilters
    {
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }
        public string FilterBy { get; set; }

        public VehicleFilters(string searchString, string currentFilter)
        {
            CurrentFilter = currentFilter;
            SearchString = searchString;
        }

        public bool ShouldApplyFilters()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                FilterBy = SearchString;
                return true;
            }
            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                FilterBy = CurrentFilter;
                return true;
            }
            return false;
        }
    }
}