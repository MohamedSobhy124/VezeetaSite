using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace Vezeeta.Models
{
    public class Insurance
    {
        [Key]
        public int InsuranceID { get; set; }
        [Required]
        [MaxLength(30), MinLength(3)]
        

        public string CompanyName { get; set; }


        //Navigation Properties
        public List<Doctor> Doctor { get; set; } = new List<Doctor>();

    }
}