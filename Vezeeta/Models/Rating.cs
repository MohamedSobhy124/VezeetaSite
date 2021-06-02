using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vezeeta.Models
{
    public class Rating
    {
        [Key]
        [Column(Order = 1)]
        public int DoctorID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int PatientID { get; set; }
        public short? EntityRating { get; set; }
        public short? AssistantRating { get; set; }
        public short? TotalRating { get; set; }
        public Doctor doctor { get; set; }
        public Patient patient { get; set; }
    }
}