using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dec_21_ASP_Bikes.Models;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Web.Security;
using Dec_21_ASP_Bikes.ViewModel;
using System.Data.Entity;





namespace Dec_21_ASP_Bikes.Controllers
{
    //Excludes authentication
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Please display the student details";
            return View();
        }

        // GET: Account


        public ActionResult Register()
        {
            return View();
        }




		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(Registration U)
		{
			if (ModelState.IsValid)
			{
				using (BikesEntities1 db = new BikesEntities1())
				{
                    //you should check duplicate registration here 
                    
					db.Registrations.Add(U);

                    db.SaveChanges();
                    
                    ModelState.Clear();
					U = null;

                   
					ViewBag.SuccessMessage = "Congratzzzz 👍👍👍👍.  Your registration was successfull";
                    ViewBag.link = "👉👉👉👉";
                }
			}
            return View(U);
		}


	


			
        

        //LoginModel


        public ActionResult Login()
        {
            return View();
        }

        //HttpPost

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (BikesEntities1 db = new BikesEntities1())
                {
                    var v = db.Registrations.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    

                    //var check1UserExits = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;
                    //var check2StatusInTable = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;

                    // db.RequestCycles.Include(c => c.CycleDetail).Single(c => c.RequestID == id);


                   ////// var Table3ToDate = db.Registrations.Include(a => a.CycleRequestedByUsers).Where(a => a.Username == User.Identity.Name).Take(1).Single().CycleRequestedByUsers.Take(1).Single().ToDate;
                  //  var cekDate = db.Registrations.Include(a => a.CycleRequestedByUsers).Where(a => a.Username == User.Identity.Name).Take(1).Single().CycleRequestedByUsers.Take(1).Single().CheckDate;




                    //var check1UserExits = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;
                    // var check2StatusInTable = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;

                    if (v != null && v.Status.Equals(true))
                    {
                        FormsAuthentication.SetAuthCookie(u.Username, false);
                        ModelState.Clear();
                        return RedirectToAction("Index", "Home");

                    }
                    else if (v != null && v.Status.Equals(false))
                    {
                        ViewBag.NotifyUser = "Sorry 😰😰😰😰" + User.Identity.Name + ". You account has been disabled."
                            + "Please contact admin for further assistance.";

                        ViewBag.SendEmail = "Send email 👉 asp.a@mail.com.";
                        return View();
                        // return Content("Your account is inactive");
                    }
                }              
            }
            return View();
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login1(DeactivationWithLoginViewModel u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (BikesEntities1 db = new BikesEntities1())
                {
                   
                    var check1UserExits = db.Registrations.Where(a => a.Username == User.Identity.Name).ToList();

                    var v = db.Registrations.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();


                   // var check2StatusInTable = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;
                    var Table3ToDate = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(a=>a.UserRequest).Take(1).Single().ToDate;
                    var cekDate = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).OrderByDescending(a => a.UserRequest).Take(1).Single().CheckDate?.Date;

                    //var check1UserExits = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;
                    // var check2StatusInTable = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Status;

                   
                    if (v != null && DateTime.Now.Date >= cekDate)
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

                    else if (v != null && DateTime.Now.Date < cekDate)
                    {
                        FormsAuthentication.SetAuthCookie(u.Username, false);
                        ModelState.Clear();
                        return RedirectToAction("Index", "Home");

                    }
                }
            }
            return View();
        }





















        //Session["LogedstudentID"] = v.StudentID.ToString();
        //Session["LogedRegistrationFullname"] = v.Fullname.ToString();
        // return RedirectToAction("AfterLogin");


        //              
        //              }
        //              else
        //              {
        //                  return RedirectToAction("Error", "Home");
        //              }
        //          }
        // }
        //return View(u);
        //  }



        //After Login
        public ActionResult AfterLogin()
        {
            if (Session["LogedStudentID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //After Login
        //After Login
        /*public ActionResult AfterLogin()
        {
            if (Session["LogedStudentID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }*/

        //Logout

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOut", "Home");
        }


        public ActionResult UserProfile()
        {
            BikesEntities1 db = new BikesEntities1();
            {
                var user = db.Registrations.Where(p => p.Username == User.Identity.Name).FirstOrDefault();
                // var user = from d in db.Registrations where (d.Username == User.Identity.Name) select d;

                //if user has cycle on his account
                ViewBag.CheckFine = " View Fine ";
				return View(user);
                
            }

        }

       // Checks if the Username exits or not while registration
        public JsonResult CheckUsername(string Username)
        {
            BikesEntities1 db = new BikesEntities1(); 
            return Json(!db.Registrations.Any(user=> user.Username == Username), JsonRequestBehavior.AllowGet);
         }

        //Checking the email
        public JsonResult CheckEmail(string Email)
        {
            BikesEntities1 db = new BikesEntities1();
            return Json(!db.Registrations.Any(user => user.Email == Email), JsonRequestBehavior.AllowGet);
        }

        //Checking Student ID on client side itself
        public JsonResult CheckStudentID(int StudentID)
        {
            BikesEntities1 db = new BikesEntities1();
            return Json(!db.Registrations.Any(user => user.StudentID == StudentID), JsonRequestBehavior.AllowGet);
        }
    }
}
