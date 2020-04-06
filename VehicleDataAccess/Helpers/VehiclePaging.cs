using VehicleDataAccess.Interfaces;

namespace VehicleDataAccess.Helpers
{
    public class VehiclePaging : IVehiclePaging
    {
        public int ResultsPerPage = 10;
        public int? Page { get; set; }
        public int ItemsToSkip { get; set; }
        public int TotalCount { get; set; }

        public VehiclePaging(int? page)
        {
            Page = page;
            ItemsToSkip = ResultsPerPage * ((Page ?? 1) - 1);
        }
    }
}