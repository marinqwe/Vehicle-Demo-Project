using System.ComponentModel.DataAnnotations;

namespace VehicleStuffDemo.ViewModels
{
    public class VehicleModelViewModel
    {
        [Key]
        public int Id { get; set; }

        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}