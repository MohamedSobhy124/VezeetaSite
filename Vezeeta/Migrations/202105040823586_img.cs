namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class img : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctors", "TitleImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "TitleImage", c => c.String());
        }
    }
}
