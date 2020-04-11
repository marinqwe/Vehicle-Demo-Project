using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VehicleDataAccess.Interfaces;

namespace VehicleDataAccess
{
    public class VehicleMake : IVehicleMake
    {
        [Key]
        public int MakeId { get; set; }

        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}