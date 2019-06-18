using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dec_21_ASP_Bikes.ViewModel
{
    public class LoginViewModel
    {
        //[Required]
        //public string Username { get; set; }

        //[Required]
        //public string Password { get; set; }



        [Required(ErrorMessage = "Please provide username", AllowEmptyStrings = false)]
        // [Remote("CheckUsername", "Account", ErrorMessage = "User name already exists. Please try another one")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please provide valid Password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be 8 char long.")]
        public string Password { get; set; }



    }
}