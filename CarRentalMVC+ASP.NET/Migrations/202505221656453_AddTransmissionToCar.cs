namespace CarRentalMVC_ASP.NET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransmissionToCar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Transmission", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Transmission");
        }
    }
}
