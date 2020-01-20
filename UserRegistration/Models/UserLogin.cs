using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserRegistration.Models
{
    public class UserLogin
    {
        [Display(Name = "Email Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Id Required")]
        public string  EmailID { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remenmber Me")]
        public bool RememberMe { get; set; }

    }
}