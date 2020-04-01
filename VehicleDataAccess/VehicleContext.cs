using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace VehicleDataAccess
{
    public class VehicleContext : DbContext
    {
        public VehicleContext()
            : base("VehicleContext")
        {
        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}