namespace Vezeeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                        PassWord = c.String(nullable: false, maxLength: 30),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        FName = c.String(nullable: false, maxLength: 10),
                        LName = c.String(nullable: false, maxLength: 10),
                        PassWord = c.String(nullable: false, maxLength: 30),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        ExamineFee = c.Int(nullable: false),
                        Title = c.Int(nullable: false),
                        Image = c.String(),
                        SpecialtyID = c.Int(nullable: false),
                        PromoCode = c.Boolean(nullable: false),
                        WaitingTime = c.Int(),
                        Phone = c.String(nullable: false, maxLength: 11),
                        branchName = c.String(),
                        AreaID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        AddressDetails = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 30),
                        Entity = c.Int(nullable: false),
                        IDImage = c.String(),
                        TitleImage = c.String(),
                        AdminID = c.Int(),
                    })
                .PrimaryKey(t => t.DoctorID)
                .ForeignKey("dbo.Admins", t => t.AdminID)
                .ForeignKey("dbo.Areas", t => t.AreaID, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyID, cascadeDelete: true)
                .Index(t => t.SpecialtyID)
                .Index(t => t.AreaID)
                .Index(t => t.CityID)
                .Index(t => t.AdminID);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaID = c.Int(nullable: false, identity: true),
                        AreaName = c.String(),
                        CityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AreaID)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: false)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.CityID);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        DoctorID = c.Int(nullable: false),
                        PatientID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.DoctorID, t.PatientID })
                .ForeignKey("dbo.Doctors", t => t.DoctorID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.DoctorID)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.InsuranceDoctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false),
                        InsuranceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DoctorID, t.InsuranceID })
                .ForeignKey("dbo.Doctors", t => t.DoctorID, cascadeDelete: true)
                .ForeignKey("dbo.Insurances", t => t.InsuranceID, cascadeDelete: true)
                .Index(t => t.DoctorID)
                .Index(t => t.InsuranceID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        PassWord = c.String(nullable: false, maxLength: 30),
                        Gender = c.Int(nullable: false),
                        Address = c.String(maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 30),
                        AdminID = c.Int(),
                        DoctorID = c.Int(),
                    })
                .PrimaryKey(t => t.PatientID)
                .ForeignKey("dbo.Admins", t => t.AdminID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID)
                .Index(t => t.AdminID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        DoctorID = c.Int(nullable: false),
                        PatientID = c.Int(nullable: false),
                        EntityRating = c.Short(),
                        AssistantRating = c.Short(),
                        TotalRating = c.Short(),
                    })
                .PrimaryKey(t => new { t.DoctorID, t.PatientID })
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .ForeignKey("dbo.Doctors", t => t.DoctorID, cascadeDelete: true)
                .Index(t => t.DoctorID)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        SpecialtyID = c.Int(nullable: false, identity: true),
                        Specilty = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.SpecialtyID);
            
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
            
            CreateTable(
                "dbo.Insurances",
                c => new
                    {
                        InsuranceID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.InsuranceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InsuranceDoctors", "InsuranceID", "dbo.Insurances");
            DropForeignKey("dbo.Doctors", "SpecialtyID", "dbo.Specialties");
            DropForeignKey("dbo.Ratings", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Ratings", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Patients", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Patients", "AdminID", "dbo.Admins");
            DropForeignKey("dbo.InsuranceDoctors", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Appointments", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Doctors", "AreaID", "dbo.Areas");
            DropForeignKey("dbo.Areas", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Doctors", "AdminID", "dbo.Admins");
            DropIndex("dbo.Ratings", new[] { "PatientID" });
            DropIndex("dbo.Ratings", new[] { "DoctorID" });
            DropIndex("dbo.Patients", new[] { "DoctorID" });
            DropIndex("dbo.Patients", new[] { "AdminID" });
            DropIndex("dbo.InsuranceDoctors", new[] { "InsuranceID" });
            DropIndex("dbo.InsuranceDoctors", new[] { "DoctorID" });
            DropIndex("dbo.Appointments", new[] { "PatientID" });
            DropIndex("dbo.Appointments", new[] { "DoctorID" });
            DropIndex("dbo.Areas", new[] { "CityID" });
            DropIndex("dbo.Doctors", new[] { "AdminID" });
            DropIndex("dbo.Doctors", new[] { "CityID" });
            DropIndex("dbo.Doctors", new[] { "AreaID" });
            DropIndex("dbo.Doctors", new[] { "SpecialtyID" });
            DropTable("dbo.Insurances");
            DropTable("dbo.AppointmentFormViewModels");
            DropTable("dbo.Specialties");
            DropTable("dbo.Ratings");
            DropTable("dbo.Patients");
            DropTable("dbo.InsuranceDoctors");
            DropTable("dbo.Appointments");
            DropTable("dbo.Cities");
            DropTable("dbo.Areas");
            DropTable("dbo.Doctors");
            DropTable("dbo.Admins");
        }
    }
}
