namespace VehicleDataAccess.Interfaces
{
    internal interface IVehicleSorting
    {
        string SortBy { get; set; }
        string SortByName { get; set; }
        string SortByAbrv { get; set; }
        string SortById { get; set; }
    }
}