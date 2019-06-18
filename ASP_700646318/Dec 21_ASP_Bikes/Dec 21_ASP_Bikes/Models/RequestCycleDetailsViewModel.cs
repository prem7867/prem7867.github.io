using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Models
{
    public class RequestCycleDetailsViewModel
    {
        public int RequestID { get; set; }
        public int CycleID { get; set; }
        public string CycleType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
        public IEnumerable<SelectListItem> CycleDetail { get; set; }
    }

}