namespace VehicleDataAccess.Helpers
{
    public class VehicleModelPaging
    {
        public int? Page { get; set; }
        public int ResultsPerPage = 10;
        public int ItemsToSkip { get; set; }
        public int TotalCount { get; set; }

        public VehicleModelPaging(int? page)
        {
            Page = page;
            ItemsToSkip = ResultsPerPage * ((Page ?? 1) - 1);
        }
    }
}