using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dec_21_ASP_Bikes.Models;

namespace Dec_21_ASP_Bikes.ViewModel
{
	public class NewModel
    {

		public List<Registration> Registrations { get; set; }
		public List<CycleRequestedByUser> CycleRequestedByUsers { get; set; }
        



    }
}