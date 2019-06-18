using Dec_21_ASP_Bikes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Controllers
{
    public class EligibilityCriteriaController : Controller
    {
        private BikesEntities1 db = new BikesEntities1();
        // GET: EligibilityCriteria
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult RestrictUserFromRequestingAnotherCycle1()

        {

            var cycleList = db.CycleRequestedByUsers.ToList();
            return View(cycleList);


        }


        // This ActionMethod restricts the user based on the criteria by checking if the user already has the cycle on his account or if he is borrowing for the first time
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
                    return Content("Cogratz.You are eligible to borrow a cycle");

                }



                var check1UserExits = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;

                var check2StatusInTable = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().Status;


                if (check1UserExits.Equals(User.Identity.Name) && check2StatusInTable.Equals(true))
                {
                    //return RedirectToAction("RestrictUserFromRequestingAnotherCycle", "Test");
                    return Content("Sorry. You are not eligible since you already have a cyle with you");
                }
                else if (check1UserExits.Equals(User.Identity.Name) && check2StatusInTable.Equals(false))
                {
                    // return RedirectToAction("RequestMe", "Test");

                    return Content("Congratz. You are eligible since you have returned the previous one");

                }
                //else 

                //else
                //{
                //    if (check2NoUsername == null || check2NoUsername.Equals(false))
                //    {
                //        return Content("Cogratz.You are eligible to borrow a cycle");
                //    }
                //}
            }

            catch (Exception ex)
            {
                throw ex;

                // ViewBag.EligibleUser = " Cogratz. You are eligible to borrow a cycle";
            }

            return View();
        }



        //This is the Method for Returning a Cycle - The table will get updated and the user will be notified based on the request

        public ActionResult ReturnCycle(int id)

        {
            try
            {
                ViewBag.userRequest1 = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name && a.RequestID == id).OrderByDescending(x => x.RequestID).Take(1).Single();
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
        public ActionResult ReturnCycle(CycleRequestedByUser cbru, int id)
        {
            // var ddreq = Session["ViewRequest"];
            //var ddreq = Session["ViewRequest"];
            BikesEntities1 db = new BikesEntities1();

            //CycleRequestedByUser task = new CycleRequestedByUser();

            var updateStatus = db.CycleRequestedByUsers.SingleOrDefault(w => w.Username == User.Identity.Name && w.RequestID == id);

            updateStatus.Status = false;
            //var cycleReturnedTime = DateTime.Now.Date.();
            updateStatus.CheckDate = DateTime.Now.Date;

            db.Entry(updateStatus).State = EntityState.Modified;

            db.SaveChanges();
            return View();

        }



        //UserProfileData for all 3 scenarios - when cycle borrowed, when not borrowed and when the username is not present in the CycleRequestedByUsers Table
        public ActionResult UserProfileWithCycleDetails1()
        {
            BikesEntities1 db = new BikesEntities1();
            try
            {
                //var cycleList = db.CycleRequestedByUsers.ToList();

                List<CycleRequestedByUser> IfUsernameExitsInTable = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).ToList();


                if (IfUsernameExitsInTable.Count.Equals(0) || IfUsernameExitsInTable.Equals(false))
                {
                    return Content("Cogratz.You are eligible to borrow a cycle");

                }

                //List<CycleRequestedByUser> check1UserExits = db.CycleRequestedByUsers.Where(a => a.Username != User.Identity.Name).ToList();
                var HasCycleOnAccountOrNot = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().Status;
                var UserWithCycleDetailsData = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single();

                //var ifUserHasCycleOnAccount = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().Status;
                //var IfUsernameExitsInTable = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;

                // List<CycleRequestedByUser> check2UserName = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).ToList();



                //  var check2Username = db.CycleRequestedByUsers.Where!(a => a.Username == User.Identity.Name);



                if (HasCycleOnAccountOrNot.Equals(true))
                {
                    //return RedirectToAction("RestrictUserFromRequestingAnotherCycle", "Test");
                    // return Content("Hi " + User.Identity.Name + ":" + "You have not borrowed any Cycles");

                    return View(UserWithCycleDetailsData);

                }

                else if (HasCycleOnAccountOrNot.Equals(false))
                {
                    // return RedirectToAction("RequestMe", "Test");

                    return Content("Hi " + User.Identity.Name + ":" + "You have not borrowed any Cycles");

                }

                //var TestNoDataUser != check2NoUsername.ToList();




                //else if (!IfUsernameExitsInTable.Equals(User.Identity.Name))
                //{
                //    return Content("Hi " + User.Identity.Name + ":" + "Please click continue to borrowed a Cycles");
                //}

            }

            catch (Exception)
            {

                // ViewBag.EligibleUser = " Cogratz. You are eligible to borrow a cycle";
            }

            return View();
        }





    }
}