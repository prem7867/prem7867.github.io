using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dec_21_ASP_Bikes.Models;

namespace Dec_21_ASP_Bikes.Controllers
{
    public class DateCheckValidationController : Controller
    {
        private BikesEntities1 db = new BikesEntities1();


        // GET: DateValidation
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CheckDateToReturn()
        {
            //  var loggedUser = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(u => u.UserRequest).SingleOrDefault().Username;
            var toDate = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().ToDate.Day;
            var fromDate = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().FromDate.Day;
            var checkDate = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().CheckDate?.Date;
            var toDateWithDate = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().ToDate.Date;
            var withStatus = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().Status;
            var fromDateWithDate = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().FromDate.Date;



            var fineDate = fromDate + 7;
            var timeRemaining = (toDateWithDate - DateTime.Now.Date).TotalDays;
            var fineDateWithDate = (fromDateWithDate - toDateWithDate).TotalDays;

            var fineDays = (DateTime.Now.Day - fineDate);
            var CalFine = (DateTime.Now.Day - fineDate) * 0.5;


            var cycleReturnedTime = DateTime.Now.Date.ToShortDateString();


            //if (fineDays.Equals(+fineDays))
            //{
            //    return Content("No fine");
            //}
            //else
            //{
            //    return Content("Fineeeeeeeeeeeeeeeeeeeeeeeeee");
            //}




            // var timeRemainingWithDate = timeRemaining.TotalDays;




            if ( toDate  < fineDays && withStatus.Equals(true))
        
            {
                ViewBag.dateCrossed = "Hello " + User.Identity.Name + ". " +
                                      " Your time to return the 🚲 has exceeded." +
                                      "Please return it immediately";
                ViewBag.fineCharges = " You have a fine of " + CalFine + "$ on your account"; //🚲
                ViewBag.FromAndToDetails = "Since you were supposed to return on " + toDateWithDate.ToShortDateString() + "." +
                                               " You have exceeded by" + (-timeRemaining) + " days.";

            }



           else if ( toDateWithDate == DateTime.Now.Date || withStatus.Equals(true))
            {
                ViewBag.todayDate = "Hello " + User.Identity.Name + ". " +
                                    " You have to return your 🚲 today.";

                // ViewBag.returnMessage = "🚲";

            }

           else if (  DateTime.Now.Date < toDateWithDate || withStatus.Equals(true))
            {
                ViewBag.uhaveTime = "Hello " + User.Identity.Name + ". " +
                                    " You have still " + timeRemaining + " days to return your 🚲.";

                // ViewBag.returnMessage = "🚲";

            }

            else
            {
                ViewBag.HappyCycling = "";
            }

            return View();
        }


    }
}