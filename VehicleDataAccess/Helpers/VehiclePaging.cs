using VehicleDataAccess.Interfaces;

namespace VehicleDataAccess.Helpers
{
    public class VehiclePaging : IVehiclePaging
    {
        public int ResultsPerPage { get; set; }
        public int? Page { get; set; }
        public int ItemsToSkip { get; set; }
        public int TotalCount { get; set; }

        public VehiclePaging(int? page)
        {
            ResultsPerPage = 10;
            Page = page;
            ItemsToSkip = ResultsPerPage * ((Page ?? 1) - 1);
        }
    }
}