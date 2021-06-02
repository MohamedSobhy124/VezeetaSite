namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insur : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InsuranceDoctors", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.InsuranceDoctors", "InsuranceID", "dbo.Insurances");
            DropIndex("dbo.InsuranceDoctors", new[] { "DoctorID" });
            DropIndex("dbo.InsuranceDoctors", new[] { "InsuranceID" });
            AddColumn("dbo.Doctors", "InsuranceID", c => c.Int(nullable: true));
            CreateIndex("dbo.Doctors", "InsuranceID");
            AddForeignKey("dbo.Doctors", "InsuranceID", "dbo.Insurances", "InsuranceID", cascadeDelete: true);
            DropTable("dbo.InsuranceDoctors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InsuranceDoctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false),
                        InsuranceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DoctorID, t.InsuranceID });
            
            DropForeignKey("dbo.Doctors", "InsuranceID", "dbo.Insurances");
            DropIndex("dbo.Doctors", new[] { "InsuranceID" });
            DropColumn("dbo.Doctors", "InsuranceID");
            CreateIndex("dbo.InsuranceDoctors", "InsuranceID");
            CreateIndex("dbo.InsuranceDoctors", "DoctorID");
            AddForeignKey("dbo.InsuranceDoctors", "InsuranceID", "dbo.Insurances", "InsuranceID", cascadeDelete: true);
            AddForeignKey("dbo.InsuranceDoctors", "DoctorID", "dbo.Doctors", "DoctorID", cascadeDelete: true);
        }
    }
}
