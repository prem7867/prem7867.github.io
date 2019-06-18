using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Dec_21_ASP_Bikes.Models;

namespace Dec_21_ASP_Bikes.Models
{
   // [MetadataType(typeof(Login))]
    public partial class Registration
    {
    }

    public class Login
    {
        

        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
    }
}

