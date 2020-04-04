using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleDataAccess
{
    public class VehicleMake
    {
        [Key]
        public int MakeId { get; set; }

        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}