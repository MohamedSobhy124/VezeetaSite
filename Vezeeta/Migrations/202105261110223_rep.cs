namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "IsRepeated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "IsRepeated");
        }
    }
}
