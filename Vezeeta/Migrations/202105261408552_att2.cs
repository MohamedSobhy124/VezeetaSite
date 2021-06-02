namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class att2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "IsAttend", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "IsAttend");
        }
    }
}
