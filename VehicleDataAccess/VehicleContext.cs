using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace VehicleDataAccess
{
    public class VehicleContext : DbContext
    {
        public VehicleContext()
            : base("VehicleContext")
        {
            // Write queries to debug output window
            Database.Log = sql => Debug.Write(sql);
        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}