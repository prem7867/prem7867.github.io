using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dec_21_ASP_Bikes.Models
{
    public class CycleDetailService
    {
        public class MemberService
        {
            private static List<CycleDetail> cd;
            static MemberService()
            {
                // Create some dummy members
                cd = new List<CycleDetail>();
            }

            public static List<CycleDetail> GetCycleDetail()
            {
                return cd;
            }
        }

    }
}