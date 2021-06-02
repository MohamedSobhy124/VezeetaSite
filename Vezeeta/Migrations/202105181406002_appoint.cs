namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appoint : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Appointments");
            AddColumn("dbo.Appointments", "AppointmentID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Appointments", "Status", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Appointments", "AppointmentID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Appointments");
            DropColumn("dbo.Appointments", "Status");
            DropColumn("dbo.Appointments", "AppointmentID");
            AddPrimaryKey("dbo.Appointments", new[] { "DoctorID", "PatientID" });
        }
    }
}
