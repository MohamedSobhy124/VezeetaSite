namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "PassWord", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "PassWord", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
