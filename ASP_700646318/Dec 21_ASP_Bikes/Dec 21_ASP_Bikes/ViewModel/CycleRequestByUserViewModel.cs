using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dec_21_ASP_Bikes.Models;
using System.ComponentModel.DataAnnotations;

namespace Dec_21_ASP_Bikes.ViewModel
{
    public class CycleRequestByUserViewModel
    {
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        public int CycleID { get; set; }

        public int RequestID { get; set; }

        public int UserRequest { get; set; }
    }

    
       
}