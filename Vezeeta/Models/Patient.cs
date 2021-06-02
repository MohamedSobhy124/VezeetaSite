using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vezeeta.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        [Required]
        [MaxLength(30), MinLength(3)]
        [Display(Name = "اسم")]
        public string Name { get; set; }

        [MaxLength(30), MinLength(6)]
        [Display(Name = "كلمة المرور")]
        public string PassWord { get; set; }

        [Display(Name =  "النوع")]
        public Gender Gender { get; set; }

        [MaxLength(50), MinLength(3)]
        [Display(Name = "العنوان")]
        public string Address { get; set; }

        [Display(Name = "تاريخ الميلاد")]
        [DataType(DataType.Date)]

        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "الموبايل")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(30), MinLength(5)]
        [Display(Name = "الايميل")]
        public string Email { get; set; }

        // Foreign Keys 
        [ForeignKey("Admin") ]

        public int? AdminID { get; set; }
        public Admin Admin { get; set; }

        [ForeignKey("doctor")]

        public int? DoctorID { get; set; }

        //Navigation Properties

        public Doctor doctor { get; set; }
        public List<Rating> PatientRatings { get; set; } = new List<Rating>();
        public List<Appointment> PatientApointments { get; set; } = new List<Appointment>();
    }
}