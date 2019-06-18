using Dec_21_ASP_Bikes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Dec_21_ASP_Bikes.ViewModel;
using System.Net;
using System.Web.Security;

namespace Dec_21_ASP_Bikes.Controllers
{
    public class TestController : Controller
    {
        //  // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //HttpGet
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(RequestCycle reg)
        {
            if (ModelState.IsValid)
            {
                BikesEntities1 db = new BikesEntities1();
                db.RequestCycles.Add(reg);
                db.SaveChanges();
                ModelState.Clear();
                reg = null;
                ViewBag.SuccessRegistration = "Registration was successfull";
            }
            return View();
        }



        public ActionResult TestRequestCycle(int id)
        {
            var rc = new List<RequestCycle>();
            var crbu = new List<CycleRequestedByUser>();
            // {
            // new CycleRequestedByUser {RequestID = 21 },
            //new CycleRequestedByUser {CycleID = 37 },
            // new CycleRequestedByUser { FromDate = DateTime ' 03/27/2015 12:00:00') },
            //  new CycleRequestedByUser {ToDate =  
            // };

            var vm = new MixOfRequestCycleNUserRequestViewModel
            {
                //rc = rc,
                //crbu = crbu

            };

            return View(vm);
        }



        public ActionResult RegisterCycleUser(int id)
        {

            BikesEntities1 db = new BikesEntities1();
            List<CycleRequestedByUser> crequest = db.CycleRequestedByUsers.Where(s => s.RequestID == id).ToList();

            return View(crequest);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCycle(CycleRequestedByUser U)
        {
            if (ModelState.IsValid)
            {
                using (BikesEntities1 db = new BikesEntities1())
                {
                    //you should check duplicate registration here 
                    db.CycleRequestedByUsers.Add(U);
                    db.SaveChanges();
                    ModelState.Clear();
                    U = null;
                    ViewBag.registrationSuccess = "Registration was Successfull. Welcome ";
                    // return RedirectToAction("Index", "Home");
                }
            }
            return View(U);
        }

        public ActionResult Details(int? id)
        {
            BikesEntities1 db = new BikesEntities1();

            //We need a single cycle out of the list where the ID of the Cycle should be equal to the ID that we are passing into the dEtails action method

            RequestCycle singleCycleDetails = db.RequestCycles.Single(c => c.RequestID == id);
            return View(singleCycleDetails);

        }

        private BikesEntities1 db = new BikesEntities1();

        public ViewResult Index2(int id)
        {

            var courses = db.RequestCycles.Include(c => c.CycleRequestedByUsers).Single(c => c.RequestID == id);
            //ViewBag.dropdownlist = new SelectList(d);
            //ViewBag.dropdownlist = new SelectList(db.RequestCycles, "RequestID", "RequestID");
            //RequestCycle rc = db.RequestCycles.Single(c => c.RequestID == id);
            return View(courses);
        }

        [HttpPost]
        public ActionResult Index1(CycleRequestedByUser crbu)
        {
            if (ModelState.IsValid)
            {
                db.CycleRequestedByUsers.Add(crbu);
                //db.Entry(crbu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DisplayImage", "Image");
            }
            //ViewBag.dropdownlist = new SelectList(db.CycleDetails, "CycleID", "CycleID");
            return View(crbu);
        }

        public ViewResult Index3(int id)
        {

            var courses = db.RequestCycles.Include(c => c.CycleDetail).Single(c => c.RequestID == id);
            //ViewBag.dropdownlist = new SelectList(d);
            //ViewBag.dropdownlist = new SelectList(db.RequestCycles, "RequestID", "RequestID");
            //RequestCycle rc = db.RequestCycles.Single(c => c.RequestID == id);
            return View(courses);
        }


        [HttpPost]
        public ActionResult Index3(RequestCycle rbu)
        {
            BikesEntities1 db = new BikesEntities1();
            List<RequestCycle> rc = db.RequestCycles.ToList();

            int rid = rbu.RequestID;
            int cid = rbu.CycleID;
            DateTime fd = rbu.FromDate;
            DateTime td = rbu.ToDate;

            CycleRequestedByUser crbu = new CycleRequestedByUser();

            crbu.RequestID = rbu.RequestID;
            crbu.CycleID = rbu.CycleID;
            crbu.FromDate = (DateTime)rbu.FromDate;
            crbu.ToDate = (DateTime)rbu.ToDate;

            //select and insert statement

            db.CycleRequestedByUsers.Add(crbu);
            db.SaveChanges();

            return View();


        }



        public ActionResult NavigateToRequestPage1(int id)
        {
            ViewBag.dropdownlist = new SelectList(db.RequestCycles, "CycleID", "CycleID");
            ViewBag.dropdownlist = new SelectList(db.RequestCycles, "RequestID", "RequestID");
            //RequestCycle singleCycleDetails = db.RequestCycles.Single(c => c.RequestID == id);
            //var rc = db.RequestCycles.Include(c=> c.Registration);
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

        public ViewResult DD(int id)
        {

            var userRequest = db.RequestCycles.Include(c => c.CycleRequestedByUsers).Single(c => c.RequestID == id);
            Session["ViewUsername"] = db.Registrations.Single(c => c.Username == User.Identity.Name);
            Session["ViewRequest"] = userRequest;

            return View(userRequest);

        }


        [HttpPost]

        public ActionResult DD()
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
            CycleRequestedByUser task = new CycleRequestedByUser();

            task.RequestID = ((Dec_21_ASP_Bikes.Models.RequestCycle)ddreq).RequestID;
            task.CycleID = ((Dec_21_ASP_Bikes.Models.RequestCycle)ddreq).CycleID;
            task.FromDate = ((Dec_21_ASP_Bikes.Models.RequestCycle)ddreq).FromDate;
            task.ToDate = ((Dec_21_ASP_Bikes.Models.RequestCycle)ddreq).ToDate;
            task.Username = ((Dec_21_ASP_Bikes.Models.Registration)userddreq).Username;

            be.CycleRequestedByUsers.Add(task);
            //}
            be.SaveChanges();
            return View();

            //}
        }

        //public ViewResult UserProfileWithCycleDetails()
        //{

        //    //TestViewModel db = new TestViewModel();
        //        var userRequest1 = db.CycleRequestedByUsers.Include(c => c.Registration).Single(c => c.Username == User.Identity.Name);
        //    //var userRequest2 = db.Registrations.Single(c => c.Username == User.Identity.Name);
        //        Session["ViewRequest"] = userRequest1;
        //       // Session["ViewUsername"] = userRequest2;
        //        return View(userRequest1);



        //    }
        public ViewResult UserProfileWithCycleDetails()
        {


            try
            {


                //ViewBag.userRequest1 = db.CycleRequestedByUsers.Include(c => c.Registration).Single(c => c.Username == User.Identity.Name) ;
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

        public ActionResult TestingUniqueRequestID(int id)
        {
            BikesEntities1 db = new BikesEntities1();
            //List<RequestCycle> crequest = db.RequestCycles.Where(s => s.RequestID == id).ToList();
            // List<RequestCycle> crequest = db.RequestCycles.Include(c => c.CycleRequestedByUsers).Where(s => s.CycleID == id).ToList();


            //var query = db.RequestCycles.Join <RequestCycle, CycleRequestedByUser, int, RequestCycle>
            //    ( db.CycleRequestedByUsers, a => a.RequestID, b => b.RequestID,(a, b) => a).ToList();


            //ViewBag.towns = new SelectList(towns.Where(x => x.Name == x.Name.Distinct()).ToList(), "Value", "Text");
            //ViewBag.towns = new SelectList(towns.Where(x => x.Name == x.Name.Distinct()).ToList(), "Value", "Text");
            //var query = db.RequestCycles.Select(i => i.RequestID).Distinct();

            //var query = (from u in db.CycleRequestedByUsers join r in db.RequestCycles on u.RequestID equals r.RequestID  select r).Distinct();

            //var query = (from u in db.CycleRequestedByUsers join r in db.RequestCycles on u.RequestID equals r.RequestID select r).Distinct();

            //var query = from w in db.RequestCycles
            //            join c in db.CycleRequestedByUsers on w.RequestID equals c.RequestID
            //            where w.RequestID != c.RequestID
            //            select w;

            List<RequestCycle> crequest = db.RequestCycles.Where(s => s.CycleID == id).ToList();


            //var query = (from r in db.RequestCycles
            //             where !(from u in db.CycleRequestedByUsers
            //                     select u.RequestID).Contains(r.RequestID)
            //             select r).Distinct();

            var query = (from r in crequest
                         where !(from u in db.CycleRequestedByUsers
                                 select u.RequestID).Contains(r.RequestID)
                         select r).Distinct();



            return View(query);
        }


































        /* public ActionResult UserProfileWithCycleDetails1()
         {
             try
             {
                 //  ViewBag.userRequest1 = db.CycleRequestedByUsers.Include(c => c.Registration).Single(c => c.Username == User.Identity.Name);
                 var userRequestStatus = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single().Status;
                 List<CycleRequestedByUser> ifUsernotPresent = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).ToList();

                 if (userRequestStatus == true)
                 {

                     return View(userRequestStatus);
                 }


                 else if (!ifUsernotPresent.Equals(true) || userRequestStatus.Equals(false))

                 {

                         ViewBag.NoCyclesOnAccount = "The User has not borrowed a Cycle";
                 }
             }
             catch (Exception)
             {
                 ViewBag.Error = "Email cannot be sent. Please check the error and try again";
             }
             return View();

         }*/



        //UserProfileData for all 3 scenarios - when cycle borrowed, when not borrowed and when the username is not present in the CycleRequestedByUsers Table
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
                    ViewBag.EligibleWhoNeverBorrowed = "Hey " + User.Identity.Name + "👋👋👋👋" + " You are never borrowed a 🚲 before."
                                                     + " Please click the below link if you wish to view the 🚲 available";

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
                        " 😎👌😊 " + " You are an eligible user since you have returned the previous one." +
                        " Please click the below link to view the 🚲 available";

                    ViewBag.link = "👉👉👉👉";

                    //return Content("Congratz. You are eligible since you have returned the previous one");
                    //  return View();

                }


                else if (HasCycleOnAccountOrNot.Equals(true))
                {
                    //return RedirectToAction("RestrictUserFromRequestingAnotherCycle", "Test");
                    // return Content("Hi " + User.Identity.Name + ":" + "You have not borrowed any Cycles");

                    return View(UserWithCycleDetailsData);

                }










            }

            catch (Exception)
            {

                // ViewBag.EligibleUser = " Cogratz. You are eligible to borrow a cycle";
            }

            return View();
        }















        //Return Cycle



        public ActionResult CycleBorrowed()

        {

            var noCycleBorrowed = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single();
            if (noCycleBorrowed.Status == true)
            {
                return View(ViewBag.noCycleBorrowed);
            }

            else
            {
                ViewBag.Error = "User has not borrowed a 🚲 ";
            }
            return View();


        }








        public ActionResult ReturnCycle()

        {
            try
            {
                ViewBag.userRequest1 = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.RequestID).Take(1).Single();
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

            db.Entry(updateStatus).State = EntityState.Modified;

            db.SaveChanges();
            return View();




        }




        //HTTPGet

        public ActionResult RestrictUserFromRequestingAnotherCycle1()

        {

            var cycleList = db.CycleRequestedByUsers.ToList();
            return View(cycleList);


        }


        //Dont allow the user to request for another cycle he has already borrowed one cycle

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

                    ViewBag.EligibleUserWhoREturnedCycle = "Heyyy " + User.Identity.Name + "😎👌." + "You are eligible to borrow a 🚲 since you have returned the previous one" +
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



        //Disable User Account



            [HttpPost]
        public ActionResult DisableAccount()
        {
           
            // var UserActiveUntil = TodateFromTable3 + 15;
            var check1UserExits = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;
            var check2StatusInTable = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;
            var Table3ToDate = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).Take(1).Single().ToDate.Day;
            var greater = (Table3ToDate + 15);

            if (DateTime.Now.Day >= greater)
            {




                var updateStatus = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single();
                //var updateStatus = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;
                updateStatus.Status = false;
                // updateStatus.CheckDate = DateTime.Now.Date;

                db.Entry(updateStatus).State = EntityState.Modified;

                db.SaveChanges();
                ViewBag.NotifyUser = "Sorry 😰😰😰😰" + User.Identity.Name + ". You account has been disabled."
                               + " Please contact admin for further assistance.";

                ViewBag.SendEmail = "";
            }
            else
            {
                return Content("Hi");

            }

            //if (check2StatusInTable.Equals(false))
            //{
            //    ViewBag.NotifyUser = "Sorry 😰😰😰😰" + User.Identity.Name + ". You account has been disabled."
            //        + " Please contact admin for further assistance.";

            //    ViewBag.SendEmail = "";
            //}
            //else
            //{
            //    return Content("Login", "Account");
            //}

            return View();
        }




        //FinalDeactivationCheck

        public ActionResult FinalDeactivationCheck()
        {
            ////var getList = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(u => u.UserRequest).Take(1).Single().Username;
            ////var ToDateGrater = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(u => u.UserRequest).Take(1).Single().ToDate.Day;
            ////var chekDate = ToDateGrater + 15;
            ////if(getList.Equals(User.Identity.Name) && ToDateGrater.Equals(chekDate))
            ////{
            ////    return Content("User should be inactive");
            ////}
            ////else[dbo].[Registration]
            ////{
            ////    return Content("User Should be Active");
            ////}



            var check1UserExits = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;
            var check2StatusInTable = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;
            var Table3ToDate = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(u=>u.UserRequest).Take(1).Single().ToDate.Date;
            var cekDate = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(u => u.UserRequest).Take(1).Single().CheckDate?.Date;

          //  var greater = cekDate;
           // var Table3ToDate = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).Take(1).Single().ToDate.Date;

           // var greaterWithDate = Conve
            if (DateTime.Now.Date >= cekDate)
            {




                var updateStatus = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single();
                //var updateStatus = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;
                updateStatus.Status = false;
                // updateStatus.CheckDate = DateTime.Now.Date;

                db.Entry(updateStatus).State = EntityState.Modified;

                db.SaveChanges();
                ViewBag.NotifyUser = "Sorry 😰😰😰😰" + User.Identity.Name + ". You account has been disabled."
                               + " Please contact admin for further assistance.";

                ViewBag.SendEmail = "";
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }



            return View();
        }













        //Login with Deactivation


        //LoginModel


        public ActionResult Login()
        {
            return View();
        }

        //HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(DeactivationWithLoginViewModel u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (BikesEntities1 db = new BikesEntities1())
                {
                    var v = db.Registrations.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();


                    var check1UserExits = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;
                    var check2StatusInTable = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;

                    // db.RequestCycles.Include(c => c.CycleDetail).Single(c => c.RequestID == id);


                    var Table3ToDate = db.Registrations.Include(a=>a.CycleRequestedByUsers).Where(a => a.Username == User.Identity.Name).Take(1).Single().CycleRequestedByUsers.Take(1).Single().ToDate;
                    var cekDate = db.Registrations.Include(a => a.CycleRequestedByUsers).Where(a => a.Username == User.Identity.Name).Take(1).Single().CycleRequestedByUsers.Take(1).Single().CheckDate;


                    //var check1UserExits = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;
                    // var check2StatusInTable = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;

                    if (v != null && v.Status.Equals(true))
                    {
                        FormsAuthentication.SetAuthCookie(u.Username, false);
                        ModelState.Clear();
                        return RedirectToAction("AboutUs", "Home");

                    }
                    else if (v != null && DateTime.Now.Date >= cekDate)
                    {

                        var updateStatus = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single();
                        //var updateStatus = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;
                        updateStatus.Status = false;
                        // updateStatus.CheckDate = DateTime.Now.Date;

                        db.Entry(updateStatus).State = EntityState.Modified;

                        db.SaveChanges();
                        ViewBag.NotifyUser = "Sorry 😰😰😰😰" + User.Identity.Name + ". You account has been disabled."
                                       + " Please contact admin for further assistance.";

                        ViewBag.SendEmail = "";
                        return View();
                        // return Content("Your account is inactive");
                    }
                }
            }
            return View();
        }




















    }



}





//var updateStatus = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(x => x.UserRequest).Take(1).Single();

//updateStatus.Status = false;
//            updateStatus.CheckDate = DateTime.Now.Date;

//            db.Entry(updateStatus).State = EntityState.Modified;

//            db.SaveChanges();

//            ViewBag.ReturnedCycle = "Hi " + User.Identity.Name +
//                        " 😎" + " You have successfully returned the 🚲 ." +
//                        " Please click the below link to request for a new 🚲";

//            ViewBag.link = "👉👉👉👉";


//            return View();



















