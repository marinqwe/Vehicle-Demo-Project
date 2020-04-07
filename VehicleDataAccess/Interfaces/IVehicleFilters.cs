namespace VehicleDataAccess.Interfaces
{
    internal interface IVehicleFilters
    {
        string SearchString { get; set; }
        string CurrentFilter { get; set; }
        string FilterBy { get; set; }

        bool ShouldApplyFilters();
    }
}