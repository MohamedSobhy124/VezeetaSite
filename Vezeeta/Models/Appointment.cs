using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vezeeta.ViewModel;

namespace Vezeeta.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        public int DoctorID { get; set; }


        public int PatientID { get; set; }
        public AppointmentStatus Status { get; set; }


        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set ; }
        public AppointmentStatus IsAttend { get; set; }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}