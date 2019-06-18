using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dec_21_ASP_Bikes.Models;

namespace Dec_21_ASP_Bikes.ViewModel
{
    public class FirstAndLastViewModel
    {
        public Registration RVM { get; set; }
        public CycleRequestedByUser CVM { get; set; }

        public string Fullname { get; set; }
        public long StudentID { get; set; }
        public string Username { get; set; }
        public int UserRequest { get; set; }
        public int RequestID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}