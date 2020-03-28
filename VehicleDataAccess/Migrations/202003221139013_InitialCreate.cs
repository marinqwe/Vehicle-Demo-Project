namespace VehicleDataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleMake",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Abrv = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.VehicleModel",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    MakeId = c.Int(nullable: false),
                    Name = c.String(),
                    Abrv = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.VehicleModel");
            DropTable("dbo.VehicleMake");
        }
    }
}