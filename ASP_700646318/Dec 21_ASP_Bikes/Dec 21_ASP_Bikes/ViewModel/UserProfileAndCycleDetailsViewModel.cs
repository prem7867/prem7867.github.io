using Dec_21_ASP_Bikes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dec_21_ASP_Bikes.ViewModel
{
    public class UserProfileAndCycleDetailsViewModel
    {
        public string Fullname { get; set; }
        public long StudentID { get; set; }
        public string Username { get; set; }
       public int UserRequest { get; set; }
        public int RequestID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public List<Registration> Registrations { get; set; }

        // public List<Registration> Registrations { get; set; }

        // public List<CycleRequestedByUser> CycleRequestedByUsers { get; set; }



    }
}