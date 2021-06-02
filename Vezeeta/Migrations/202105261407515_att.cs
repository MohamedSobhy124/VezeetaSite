namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class att : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "IsAttend");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "IsAttend", c => c.Boolean(nullable: false));
        }
    }
}
