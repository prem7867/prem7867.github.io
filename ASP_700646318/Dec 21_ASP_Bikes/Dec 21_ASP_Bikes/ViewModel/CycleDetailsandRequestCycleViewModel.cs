using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dec_21_ASP_Bikes.ViewModel
{
    public class CycleDetailsandRequestCycleViewModel
    {
        public int CycleID { get; set; }
        public byte[] CycleImage { get; set; }
        public string CycleAccessories { get; set; }
        public string CycleType { get; set; }
        public int RequestID { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
    }
}