using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dec_21_ASP_Bikes.Models;

namespace Dec_21_ASP_Bikes.Controllers
    {
    public class FineCalculationController : Controller
    {
            private BikesEntities1 db = new BikesEntities1();


            // GET: DateValidation
            public ActionResult Index()
            {
                return View();
            }


            public ActionResult CalculateFine()
            {

            List<CycleRequestedByUser> IfUsernameExitsInTable = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).ToList();
            //var TestNoDataUser != check2NoUsername.ToList();
            //var withStatusAgain = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().Status;

            if (IfUsernameExitsInTable.Count.Equals(0) || IfUsernameExitsInTable.Equals(false))
            {
                ViewBag.NoFine = "Hello User. You have no charges on your account.";
            }
            else
            {


                //  var loggedUser = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(u => u.UserRequest).SingleOrDefault().Username;

                var toDate = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().ToDate.Day;
                var fromDate = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().FromDate.Day;
                var checkDate = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().CheckDate?.Date;
                var withStatus = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().Status;


                var fromDateWithDate = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().FromDate.Date;
                var toDateWithDate = db.CycleRequestedByUsers.Where(u => u.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().ToDate.Date;



                var fineDate = toDate;
                var fineDatwithDate = toDateWithDate;

                var timeRemaining = -(toDateWithDate - DateTime.Now.Date).TotalDays;
                //var fineDateWithDate = (fromDateWithDate + toDateWithDate).TotalDays;

                //var fineDays = (DateTime.Now.Day - fineDate);
                var FineApplicable = (DateTime.Now.Date - fineDatwithDate).TotalDays * 0.5;



                if (DateTime.Now.Date > toDateWithDate && withStatus.Equals(true))

                {
                    ViewBag.dateCrossed = "Hello " + User.Identity.Name + ". " +
                                          " Your time to return the 🚲 has exceeded." +
                                          "Please return it immediately";
                    ViewBag.fineCharges = " You have a fine of " + FineApplicable + "$ on your account"; //🚲
                    ViewBag.FromAndToDetails = "Since you were supposed to return your 🚲 on " + toDateWithDate.ToShortDateString() + "." +
                                                   " You have exceeded by " + (timeRemaining) + " days.";

                }



                else if (DateTime.Now.Date == toDateWithDate || withStatus.Equals(true))
                {
                    ViewBag.uhaveTime = "Hello " + User.Identity.Name + ". " +
                                         " You have to return your 🚲 today..";


                    // ViewBag.returnMessage = "🚲";

                }

                else if (DateTime.Now.Date < toDateWithDate | withStatus.Equals(true))
                {
                    ViewBag.uhaveTime = "Hello " + User.Identity.Name + ". " +
                                        " You have still " + timeRemaining + " days to return your 🚲.";

                    // ViewBag.returnMessage = "🚲";

                }
                else if (withStatus.Equals(false))
                {
                    ViewBag.NoFine = "Hello User. You have no charges on you account.";
                }

            }

                return View();


            



                //var cycleReturnedTime = DateTime.Now.Date.ToShortDateString();


                //if (fineDays.Equals(+fineDays))
                //{
                //    return Content("No fine");
                //}
                //else
                //{
                //    return Content("Fineeeeeeeeeeeeeeeeeeeeeeeeee");
                //}




                // var timeRemainingWithDate = timeRemaining.TotalDays;


            }


        }
    }