namespace VehicleDataAccess.Helpers
{
    public class VehicleMakePaging
    {
        public int ResultsPerPage = 3;
        public int? Page { get; set; }
        public int ItemsToSkip { get; set; }
        public int TotalCount { get; set; }

        public VehicleMakePaging(int? page)
        {
            Page = page;
            ItemsToSkip = ResultsPerPage * ((Page ?? 1) - 1);
        }
    }
}