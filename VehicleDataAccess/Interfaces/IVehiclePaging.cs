namespace VehicleDataAccess.Interfaces
{
    internal interface IVehiclePaging
    {
        int? Page { get; set; }
        int ItemsToSkip { get; set; }
        int TotalCount { get; set; }
        int ResultsPerPage { get; set; }
    }
}