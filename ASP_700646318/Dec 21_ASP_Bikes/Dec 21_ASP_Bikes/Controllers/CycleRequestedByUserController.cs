using Dec_21_ASP_Bikes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Dec_21_ASP_Bikes.Controllers
{
	public class CycleRequestedByUserController : Controller
	{
		// GET: CycleRequestedByUser

		private BikesEntities1 db = new BikesEntities1();

		public ActionResult Index()
		{
			return View();
		}


		//Get Method: User requestes the Cycle by Clciking on the REquest Me link in the Cycle details page.

		public ActionResult NavigateToRequestPage()
		{
			ViewBag.dropdownlist = new SelectList(db.RequestCycles, "CycleID", "CycleID");
			ViewBag.dropdownlist = new SelectList(db.RequestCycles, "RequestID", "RequestID");
			
			//RequestCycle rc = db.RequestCycles.Single(c => c.RequestID == id);
			return View();

		}

		[HttpPost]
		public ActionResult NavigateToRequestPage(CycleRequestedByUser crbu)
		{
			if (ModelState.IsValid)
			{
				db.CycleRequestedByUsers.Add(crbu);
				db.SaveChanges();
				return RedirectToAction("DisplayImage", "Image");
			}
			//ViewBag.dropdownlist = new SelectList(db.CycleDetails, "CycleID", "CycleID");
			return View(crbu);
		}


		public ActionResult DisplayRequestedDetails1(int id)
		{

			var userRequest = db.RequestCycles.Include(c => c.CycleRequestedByUsers).Single(c => c.RequestID == id);
            Session["ViewUsername"] = db.Registrations.Single(c => c.Username == User.Identity.Name);
            Session["ViewRequest"] = userRequest;
            return View(userRequest);

		}


		[HttpPost]

		public ActionResult DisplayRequestedDetails1()
		{
			var ddreq = Session["ViewRequest"];
            var userddreq = Session["ViewUsername"];
            BikesEntities1 be = new BikesEntities1();
			//using (var be = new BikesEntities())
			//{
			//var x = db.RequestCycles.Include(c => c.CycleRequestedByUsers).Single(c => c.RequestID == id);

			//foreach (var mystep in ddreq)
			//var mystep = be.RequestCycles.Where(p => p.RequestID == id).FirstOrDefault();

			//{

           // var currentDate = DateTime.Now;
			CycleRequestedByUser task = new CycleRequestedByUser();
            RequestCycle rc = new RequestCycle();

            //var checkDateWithDate = DateTime.Now.AddDays(7);


            task.RequestID = ((Dec_21_ASP_Bikes.Models.RequestCycle)ddreq).RequestID;
			task.CycleID = ((Dec_21_ASP_Bikes.Models.RequestCycle)ddreq).CycleID;
			task.FromDate = ((Dec_21_ASP_Bikes.Models.RequestCycle)ddreq).FromDate;
			task.ToDate = ((Dec_21_ASP_Bikes.Models.RequestCycle)ddreq).ToDate;

            var ToDateWithDate = ((Dec_21_ASP_Bikes.Models.RequestCycle)ddreq).ToDate;


            task.Username = ((Dec_21_ASP_Bikes.Models.Registration)userddreq).Username;
            task.CheckDate = ToDateWithDate.AddDays(7);

            if (task.Username != null)
            {
                task.Status = true;
                rc.Status = true;
            }

            else
            {
                task.Status = false;
                rc.Status = false;
            }

            be.CycleRequestedByUsers.Add(task);

            

            db.Entry(rc).State = EntityState.Modified;

           // db.SaveChanges();


            //}
            be.SaveChanges();
            //db.SaveChanges();

            ViewBag.InsertedDataToTable4 = "Congtaz 👍👍👍👍" + User.Identity.Name + ". Your 🚲 request was successfull";
            ViewBag.navigationToCyclesBorrowed = "🚲";





            return View();

			//}
		}






        public ViewResult UserProfileWithCycleDetails()
        {

            //TestViewModel db = new TestViewModel();
            try
            {


                //  ViewBag.userRequest1 = db.CycleRequestedByUsers.Include(c => c.Registration).Single(c => c.Username == User.Identity.Name);
                ViewBag.userRequest1 = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single();

                //var userRequest2 = db.Registrations.Single(c => c.Username == User.Identity.Name);
                //  Session["ViewRequest"] = userRequest1;
                // Session["ViewUsername"] = userRequest2;


                return View(ViewBag.userRequest1);
                
            }
            catch (Exception)
            {
                ViewBag.Error = "Email cannot be sent. Please check the error and try again";
            }
            return View();



        }




        //[HttpPost]

        //public ViewResult UserProfileWithCycleDetails(CycleRequestedByUser cbru, int id)
        //{
        //    // var ddreq = Session["ViewRequest"];
        //    //var ddreq = Session["ViewRequest"];
        //    BikesEntities1 db = new BikesEntities1();

        //    //CycleRequestedByUser task = new CycleRequestedByUser();

        //    var updateStatus = db.CycleRequestedByUsers.SingleOrDefault(w => w.RequestID == id);
        //    updateStatus.Status = false;

        //    db.SaveChanges();
        //    return View();

    }
}


   








       


   