using Dec_21_ASP_Bikes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dec_21_ASP_Bikes.ViewModel
{
    public class UserProfileViewModel
    {
        public List<Registration> regUserProfile { get; set; }
        public List<CycleDetail> cycDetailsUserProfile { get; set; }
        public List<RequestCycle> reqCycleUserProfile { get; set; }
        

    }
}