namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ccc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contactus",
                c => new
                    {
                        IDContact = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Email = c.String(nullable: false, maxLength: 30),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDContact);
            
            AddColumn("dbo.Doctors", "isAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "isAvailable");
            DropTable("dbo.Contactus");
        }
    }
}
