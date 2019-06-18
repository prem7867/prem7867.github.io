using Dec_21_ASP_Bikes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Controllers
{
    public class EligibilityAndUserProfileController : Controller
    {
        private BikesEntities1 db = new BikesEntities1();
        // GET: EligibilityAndUserProfile
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult RestrictUserFromRequestingAnotherCycle1()

        {

            var cycleList = db.CycleRequestedByUsers.ToList();
            return View(cycleList);


        }

        public ActionResult RestrictUserFromRequestingAnotherCycle()
        {
            BikesEntities1 db = new BikesEntities1();
            try
            {
                //var cycleList = db.CycleRequestedByUsers.ToList();
                //var check2NoUsername = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;
                // var check2NoUsername = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).ToList();
                //var check2NoUsername = !((from u in db.CycleRequestedByUsers select u.Username).Contains(User.Identity.Name));
                //  var check2Username = db.CycleRequestedByUsers.Where!(a => a.Username == User.Identity.Name);

                List<CycleRequestedByUser> check2NoUsername = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).ToList();
                //var TestNoDataUser != check2NoUsername.ToList();


                if (check2NoUsername.Count.Equals(0) || check2NoUsername.Equals(false))
                {


                    // return Content("Cogratz.You are eligible to borrow a cycle");


                    ViewBag.EligibleWhoNeverBorrowed = "Cogratzzzz " + User.Identity.Name + "👍👍👍👍." + " You are eligible to borrow a 🚲 as you have never borrowed one"
                                                       + " Please click the below link to view the 🚲 available";

                    ViewBag.link = "👉👉👉👉";
                    return View();
                }



                var check1UserExits = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;

                var check2StatusInTable = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().Status;


                if (check1UserExits.Equals(User.Identity.Name) && check2StatusInTable.Equals(true))
                {
                    //return RedirectToAction("RestrictUserFromRequestingAnotherCycle", "Test");
                    //return Content("Sorry. You are not eligible since you already have a cyle with you");

                    ViewBag.UserALreadyHasBorrowed = "Sorry " + User.Identity.Name + "😰😰😰😰." + " You are not eligible since you already have a 🚲 with you";

                    ViewBag.link = "👉👉👉👉";

                    //return Content("Congratz. You are eligible since you have returned the previous one");
                    return View();
                }
                else if (check1UserExits.Equals(User.Identity.Name) && check2StatusInTable.Equals(false))
                {
                    // return RedirectToAction("RequestMe", "Test");

                    ViewBag.EligibleUserWhoREturnedCycle = "Heyyy " + User.Identity.Name + "😎👌." + "You are eligible to borrow a 🚲 since you have returned the previous one." +
                                                            " Please click the below link to view the 🚲 available";

                    ViewBag.link = "👉👉👉👉";

                    //return Content("Congratz. You are eligible since you have returned the previous one");
                    return View();

                }

            }

            catch (Exception ex)
            {
                throw ex;

                // ViewBag.EligibleUser = " Cogratz. You are eligible to borrow a cycle";
            }

            return View();
        }




        //User Profile Details

        public ActionResult UserProfileWithCycleDetails1()
        {
            BikesEntities1 db = new BikesEntities1();
            try
            {
                //var cycleList = db.CycleRequestedByUsers.ToList();

                List<CycleRequestedByUser> IfUsernameExitsInTable = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).ToList();
                // if (IfUsernameExitsInTable.Equals(null) || IfUsernameExitsInTable.Equals(false))
                if (IfUsernameExitsInTable.Count.Equals(0) || IfUsernameExitsInTable.Equals(false))
                {
                    //return Content("Cogratz.You are eligible to borrow a cycle");
                    //ViewBag.EligibleWhoNeverBorrowed = "Cogratzzzz " + User.Identity.Name + "You are eligible to borrow a cycle as you have never borrowed one";
                    ViewBag.EligibleWhoNeverBorrowed = "Hey " + User.Identity.Name + /*"👍👍👍👍" */ " 😎👌😊 "+" You have never borrowed a 🚲."
                                                     + " Please click the below link to view the 🚲 available";

                    ViewBag.link = "👉👉👉👉"; ;
                    //return View();

                }



                var UserWithCycleDetailsData = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single();
                var HasCycleOnAccountOrNot = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().Status;

                if (HasCycleOnAccountOrNot.Equals(false))
                {
                    // return RedirectToAction("RequestMe", "Test");

                    // return Content("Hi " + User.Identity.Name + ":" + "You have not borrowed any Cycles");
                    ViewBag.EligibleWhoReturnedPreviousOne = "Hi " + User.Identity.Name +
                        " 😎👌😊 " + " You have not borrowed a 🚲 ." +
                        " Please click the below link to view the 🚲 available";

                    ViewBag.link = "👉👉👉👉";

                    //return Content("Congratz. You are eligible since you have returned the previous one");
                    //  return View();

                }


                else if (HasCycleOnAccountOrNot.Equals(true))
                {
                    //return RedirectToAction("RestrictUserFromRequestingAnotherCycle", "Test");
                    // return Content("Hi " + User.Identity.Name + ":" + "You have not borrowed any Cycles");

                    //ViewBag.CheckFine = "Hey " + User.Identity.Name + ". ";
                    return View(UserWithCycleDetailsData);
                   

                }

            }

            catch (Exception)
            {

                // ViewBag.EligibleUser = " Cogratz. You are eligible to borrow a cycle";
            }

            return View();
        }



        //Returning Cycle Scenario


        public ActionResult ReturnCycle()

        {
            try
            {
                ViewBag.userRequest1 = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single();
                //var data = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single();
                return View(ViewBag.userRequest1);
                // return View(data);
            }
            catch (Exception)
            {
                ViewBag.Error = "User has not borrowed a Cycle ";
            }
            return View();
        }



        [HttpPost]
        public ActionResult ReturnCycle(CycleRequestedByUser cbru)
        {
            // var ddreq = Session["ViewRequest"];
            //var ddreq = Session["ViewRequest"];
            BikesEntities1 db = new BikesEntities1();

            //CycleRequestedByUser task = new CycleRequestedByUser();

            //var updateStatus = db.CycleRequestedByUsers.SingleOrDefault(w => w.Username == User.Identity.Name && w.RequestID == id);


           var updateStatus = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single();

            updateStatus.Status = false;
            updateStatus.CheckDate = DateTime.Now.Date;

            db.Entry(updateStatus).State = EntityState.Modified;

            db.SaveChanges();

            ViewBag.ReturnedCycle = "Hi " + User.Identity.Name +
                        " 😎" + " You have successfully returned the 🚲 ." +
                        " Please click the below link to request for a new 🚲";

            ViewBag.link = "👉👉👉👉";


            return View();




        }




    }
}