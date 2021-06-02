using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vezeeta.Models;

namespace Vezeeta.ViewModel
{
    public class RegisterViewModel
    {


        [Required(ErrorMessage = "لابد ان تدخل قيم ")]
        [Display(Name = "الاسم")]
        [MaxLength(30, ErrorMessage = "لابد ان يكون عدد الحروف اقل من 30 "), MinLength(3, ErrorMessage = "لابد عدد الحروف اكبر من 3")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لابد ان تدخل قيم ")]
        [EmailAddress]
        [Display(Name = "الايميل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لابد ان تدخل قيم ")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        [MaxLength(30, ErrorMessage = "لابد ان يكون عدد الحروف اقل من 30 "), MinLength(3, ErrorMessage = "عدد الحروف اكبر من 3")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمة المرور وتاكيد كلمة المرور غير متطابقان")]
        public string ConfirmPassword { get; set; }



    }
}