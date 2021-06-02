using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vezeeta.Models
{
    public class Contactus
    {
        [Key]
        public int IDContact { get; set; }

        [Required]
        [MaxLength(10), MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MaxLength(11), MinLength(11)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(30), MinLength(5)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }

}