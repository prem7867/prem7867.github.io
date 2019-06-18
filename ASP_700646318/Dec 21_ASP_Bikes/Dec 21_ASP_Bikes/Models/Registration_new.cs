using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Models
{
    [MetadataType(typeof(RegistrationMetaData))]
    public partial class Registration
    {
    }

    public class RegistrationMetaData

    {
        [RegularExpression(@"(-?([A-Z].\s)?([A-Z][a-z]+)\s?)+([A-Z]'([A-Z][a-z]+))?", ErrorMessage = "Name field will not allow any kind of special characters except spaces.")]
        [Required(ErrorMessage = "Please provide Full Name", AllowEmptyStrings = false)]
        public string Fullname { get; set; }

        // [DisplayAttribute(Name = "Username")]
        [Required(ErrorMessage = "Please provide username", AllowEmptyStrings = false)]
        [Remote("CheckUsername", "Account", ErrorMessage = "User name already exists. Please try another one")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please provide valid Email", AllowEmptyStrings = false)]
        [Remote("CheckEmail", "Account", ErrorMessage = "Email already exists. Please try another one")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Please provide valid email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide valid Password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be 8 char long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please provide username", AllowEmptyStrings = false)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm password dose not match.")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Remote("CheckStudentID", "Account", ErrorMessage = "StudentID already exists. Please try another one")]
        [RegularExpression(@"^[700]{3}\d{6}", ErrorMessage = "Invalid student ID. The ID should start with 700 and should be follwed by 6 characters")]
        public long StudentID { get; set; }

		public string Role { get; set; }


    }
}

