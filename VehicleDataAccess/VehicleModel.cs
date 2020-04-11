using VehicleDataAccess.Interfaces;

namespace VehicleDataAccess
{
    public class VehicleModel : IVehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int MakeId { get; set; }
        public VehicleMake VehicleMake { get; set; }
    }
}