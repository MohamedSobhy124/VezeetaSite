namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "DoctorStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "DoctorStatus");
        }
    }
}
