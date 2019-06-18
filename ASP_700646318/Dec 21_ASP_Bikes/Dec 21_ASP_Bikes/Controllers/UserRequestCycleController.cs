using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dec_21_ASP_Bikes.Models;
using System.Data.Entity;
using Dec_21_ASP_Bikes.ViewModel;

namespace Dec_21_ASP_Bikes.Controllers
{
    public class UserRequestCycleController : Controller
    {
        // GET: UserRequestCycle
        public ActionResult userRaisesRequestForCycle(int id)
        {
            BikesEntities1 db = new BikesEntities1();
            RequestCycle urc = db.RequestCycles.Single(c => c.CycleID == id);
            return View(urc);


        }

        public ActionResult Buddy()
        {
            //BikesEntities db = new BikesEntities();

            //var retrieve = (from a in db.Registrations
            //                join u in db.CycleRequestedByUsers on a.StudentID equals u.StudentID
            //                select a).Include(c => c.CycleRequestedByUsers);

            Registration reg = new Registration();
            CycleRequestedByUser cbru = new CycleRequestedByUser();


            UserProfileAndCycleDetailsViewModel upvm = new UserProfileAndCycleDetailsViewModel();
            //upvm.Fullname = reg.Fullname;
            //upvm.StudentID = reg.StudentID;
            //upvm.UserRequest = cbru.UserRequest;
            //upvm.RequestID = cbru.RequestID;

           // var userDetails1 = upvm.Registrations.Include(c => c.CycleRequestedByUsers).Single(c => c.Username == User.Identity.Name);
            //var userDetails2 = db.Registrations.Single(c => c.Username == User.Identity.Name);
           // Session["ViewRequest"] = userDetails1;
            //Session["ViewUsername"] = userDetails2;

            //return View(userDetails1);



            return View();




        }



        public ActionResult display()

        {
            BikesEntities1 db = new BikesEntities1();
            var x = db.RegistrationAndCycleRequestedByUsers.ToList();
            return View(x);
        }

            public ActionResult display1()

        {
            //var reg = new Registration();
            //var crbu = new CycleRequestedByUser();
            //FirstAndLastViewModel db = new FirstAndLastViewModel()
            //{
            //    RVM = reg,
            //    CVM = crbu

            //};







            //db.Registrations.Include(c => c.CycleRequestedByUsers);
            //List<UserProfileAndCycleDetailsViewModel> joindata = (from c in db.Registrations join n in db.CycleRequestedByUsers 
            //                                                on c.StudentID equals n.UserRequest select new UserProfileAndCycleDetailsViewModel
            //    { StudentID = c.StudentID, Fullname = c.Fullname, RequestID = n.RequestID,  FromDate = n.FromDate, ToDate = n.ToDate}).ToList();

            //return View(joindata);


            //NewModel vm = new NewModel();
            // var StudentDetails = db.Registrations.Include(c => c.CycleRequestedByUsers);
            BikesEntities1 db = new BikesEntities1();

            //var StudentDetails = (from c in db.Registrations
            //                      join e in db.CycleRequestedByUsers on c.StudentID equals e.StudentID
            //                      where c.Username == User.Identity.Name
            //                      select c.Fullname);

            

            var details = from n in db.Registrations
                          join c in db.CycleRequestedByUsers on n.Username equals c.Username

                          select new { n.Fullname, n.StudentID, c.UserRequest, c.RequestID, c.FromDate, c.ToDate };
        
    


            // return View(db);


            //Order order = db.Orders.Find(id);
            return View(details);
        }


        //public ActionResult RequestedCycleProfile()
        //{
        //    BikesEntities db = new BikesEntities();

        //    var query = db.
        //    return View(query);



        //}
    }
    }
            
  
  
