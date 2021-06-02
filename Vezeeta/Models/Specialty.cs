using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vezeeta.Models
{
    public class Specialty
    {
        [Key]
        public int SpecialtyID { get; set; }

        [Required]
        [MaxLength(30), MinLength(3)]
        public string Specilty { get; set; }

        public List<Doctor> Doctor { get; set; } = new List<Doctor>();
    }
}