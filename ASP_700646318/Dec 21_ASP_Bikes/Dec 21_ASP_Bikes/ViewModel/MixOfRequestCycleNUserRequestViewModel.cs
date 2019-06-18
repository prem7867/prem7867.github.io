using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dec_21_ASP_Bikes.Models;
using System.ComponentModel.DataAnnotations;

namespace Dec_21_ASP_Bikes.ViewModel
{
    public class MixOfRequestCycleNUserRequestViewModel
    {
		
			[Required]
			public string RequestID { get; set; }

			[Required]
			public string CycleID { get; set; }
		
			[Required]
			public string FromDate { get; set; }

			[Required]
			public string ToDate { get; set; }
		}


	}
