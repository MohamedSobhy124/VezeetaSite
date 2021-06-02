using System;
using System.Data.Entity;
using System.Linq;

namespace Vezeeta.Models
{
    public class MyModel : DbContext
    {
        // Your context has been configured to use a 'MyModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Vezeeta.Models.MyModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MyModel' 
        // connection string in the application configuration file.
        public MyModel()
            : base("name=MyModel")
        {
        }

        public virtual DbSet<Admin> Admins { set; get; }
        public virtual DbSet<Patient> Patients { set; get; }
        public virtual DbSet<Doctor> Doctors { set; get; }
        public virtual DbSet<Rating> Ratings { set; get; }
        public virtual DbSet<Appointment> Appointments { set; get; }
        public virtual DbSet<Insurance> Insurances { set; get; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<Area> Areas { set; get; }
        public virtual DbSet<Contactus> Contactus { set; get; }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

 
}