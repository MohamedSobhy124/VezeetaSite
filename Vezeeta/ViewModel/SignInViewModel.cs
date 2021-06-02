using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vezeeta.ViewModel
{
    public class SignInViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "الايميل")]
        
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public string Message { get; set; }


    }
}