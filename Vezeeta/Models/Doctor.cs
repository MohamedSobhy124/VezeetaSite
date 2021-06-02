using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vezeeta.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }

        [Required]
        [MaxLength(10), MinLength(3)]
        public string FName { get; set; }

        [Required]
        [MaxLength(10), MinLength(3)]
        public string LName { get; set; }
        [NotMapped]
        [Display(Name = "اسم الدكتور")]
        public string FullName
        {
            get
            {
                return FName + " " + LName;
            }
        }
        public AppointmentStatus DoctorStatus { get; set; }


        [Required]
        [MaxLength(30), MinLength(3)]
        public string PassWord { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public int ExamineFee { get; set; }

        [Required]
        public Title Title { get; set; }

        
        public string Image { get; set; }
        [ForeignKey("Specialty")]
        public int SpecialtyID { get; set; }
        public Specialty Specialty { get; set; }
        public bool PromoCode { get; set; }

        public int? WaitingTime { get; set; }

        [Required]
        [MaxLength(11), MinLength(11)]
        public string Phone { get; set; }
        public string  branchName { get; set; }
        [Required]
        [ForeignKey("Area")]
        public int AreaID { get; set; }
        public Area Area { get; set; }
        [Required]
        [ForeignKey("City")]
        public int CityID { get; set; }
        public City City { get; set; }
        [MaxLength(50), MinLength(3)]
        public string AddressDetails { get; set; }

        [Required]
        [MaxLength(30), MinLength(5)]
        public string Email { get; set; }
        [Required]
        public Entity Entity { get; set; }
        [Display(Name = " Image ID")]

        public string IDImage { get; set; }


        // Foreign Key Admin

        public int? AdminID { get; set; }

        //Navigation Properties
        public Admin Admin { get; set; }
        public List<Patient> DoctorPatients { get; set; } = new List<Patient>();
        public List<Rating> DoctorRatings { get; set; } = new List<Rating>();

        public List<Appointment> DoctorApointments { get; set; } = new List<Appointment>();
        [ForeignKey("Insurance")]
        public int InsuranceID { get; set; }
        public Insurance Insurance { get; set; }
    }
}