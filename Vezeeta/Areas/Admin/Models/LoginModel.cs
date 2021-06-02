using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vezeeta.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email Address")]
        public string Email { get; set; }
        [Required]

        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        public string Massage { get; set; }

    }
}