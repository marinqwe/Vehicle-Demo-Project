namespace VehicleDataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateRelationships : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.VehicleMake");
            DropColumn("dbo.VehicleMake", "Id");
            AddColumn("dbo.VehicleMake", "MakeId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.VehicleMake", "MakeId");
            CreateIndex("dbo.VehicleModel", "MakeId");
            AddForeignKey("dbo.VehicleModel", "MakeId", "dbo.VehicleMake", "MakeId", cascadeDelete: true);
        }

        public override void Down()
        {
            AddColumn("dbo.VehicleMake", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.VehicleModel", "MakeId", "dbo.VehicleMake");
            DropIndex("dbo.VehicleModel", new[] { "MakeId" });
            DropPrimaryKey("dbo.VehicleMake");
            DropColumn("dbo.VehicleMake", "MakeId");
            AddPrimaryKey("dbo.VehicleMake", "Id");
        }
    }
}