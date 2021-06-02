using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vezeeta.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        [Required]
        [MaxLength(30), MinLength(3)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(30), MinLength(3)]
        public string PassWord { get; set; }
        public string Image { get; set; }

        //Navigation Properties
        public List<Doctor> AdminDoctors { get; set; } = new List<Doctor>();
        public List<Patient> AdminPatients { get; set; } = new List<Patient>();
    }
}