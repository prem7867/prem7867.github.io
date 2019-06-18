using Dec_21_ASP_Bikes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Controllers
{
    //Excludes authentication
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private BikesEntities1 db = new BikesEntities1();
        // GET: Home
        
        public ActionResult Index()
        {
            ViewBag.success = "Welcome " + User.Identity.Name;

            ViewBag.RUEligible = "👉 R Eligible to borrow a 🚲 ❓";
           

            return View();
        }

        public ActionResult Error()
        {
           
            ViewBag.error = "Invalid Login 😰😱. Please try again.";

            return View();
        }

        public ActionResult LogOut()
        {

            //Session[username = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;

            ViewBag.loggedOut = "👉 have successfully logged out. See 👉 soon.  Have a nice day";

            return View();
        }

        // We are passing the CycleID as a parameter which will retrieve the CycleID from the CycleDetails table 
        // and will be passed to the view


        public ActionResult Details1( int? id)
        {
            BikesEntities1 db = new BikesEntities1();

            //We need a single cycle out of the list where the ID of the Cycle should be equal to the ID that we are passing into the dEtails action method



           // var query = db.CycleDetails.Join(db.rc, r => r.CycleID, p => p.CycleID, (r, p) => new { r.CycleImage, r.CycleAccessories, r.CycleType, r.CycleID, p.RequestID, p.FromDate, p.ToDate });

           CycleDetail singleCycleDetails = db.CycleDetails.Single(c => c.CycleID == id);

            return View(singleCycleDetails);

        }


        public ActionResult Details(int? id)
        {
            BikesEntities1 db = new BikesEntities1();

            //We need a single cycle out of the list where the ID of the Cycle should be equal to the ID that we are passing into the dEtails action method



            // var query = db.CycleDetails.Join(db.rc, r => r.CycleID, p => p.CycleID, (r, p) => new { r.CycleImage, r.CycleAccessories, r.CycleType, r.CycleID, p.RequestID, p.FromDate, p.ToDate });

            CycleDetail singleCycleDetails = db.CycleDetails.Single(c => c.CycleID == id);
            return View(singleCycleDetails);

        }



        //[HttpPost]
        //public ActionResult Details(int id, CycleRequestedByUser addCycleCRBU)
        //{
        //    BikesEntities1 db = new BikesEntities1();

        //    //We need a single cycle out of the list where the ID of the Cycle should be equal to the ID that we are passing into the dEtails action method

        //    db.CycleDetails.Single(c => c.CycleID == id);
        //    db.CycleRequestedByUsers.Add(addCycleCRBU);
        //    db.SaveChanges();
        //    return View();

        //}



        public ActionResult AboutUs()
        {
            return View();
        }

        /*  public JsonResult GetEvents()
          {
              //Here MyDatabaseEntities is our entity datacontext (see Step 4)
              using (BikesEntities dc = new BikesEntities())
              {
                  var v = dc.Events.OrderBy(a => a.StartDate).ToList();
                  return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

     } */

        public ActionResult ContactUs()
        {
            return View();
        }



        //Disabling Students Acount

        public ActionResult DisableAccount()
        {
            var check1UserExits = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;
            var check2StatusInTable = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;

            if(check2StatusInTable.Equals(false))
            {
                ViewBag.NotifyUser = "Sorry 😰😰😰😰" + User.Identity.Name + ". You account has been disabled."
                    + " Please contact admin for further assistance.";

                ViewBag.SendEmail = "";
            }
            else
            {
                return Content("Login", "Account");
            }

            return View();
        }





    }


    
}