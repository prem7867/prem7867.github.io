using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dec_21_ASP_Bikes.ViewModel
{
    public class RaiseRequestForCycleViewModel
    {
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        public int CycleID { get; set; }

        public int RequestID { get; set; }
    }
}
    
    
    