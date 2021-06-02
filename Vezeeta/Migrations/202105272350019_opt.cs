namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class opt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctors", "isAvailable");
            DropTable("dbo.AppointmentFormViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppointmentFormViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(nullable: false),
                        Time = c.String(nullable: false),
                        Detail = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Patient = c.Int(nullable: false),
                        Doctor = c.Int(nullable: false),
                        Heading = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Doctors", "isAvailable", c => c.Boolean(nullable: false));
        }
    }
}
