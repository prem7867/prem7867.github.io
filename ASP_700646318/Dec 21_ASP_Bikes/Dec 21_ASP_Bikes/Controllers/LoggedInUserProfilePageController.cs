using Dec_21_ASP_Bikes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dec_21_ASP_Bikes.Models;

namespace Dec_21_ASP_Bikes.Controllers
{
    public class LoggedInUserProfilePageController : Controller
    {
        // GET: LoggedInUserProfilePage
        public ActionResult Details(int id)
        {
            BikesEntities1 db = new BikesEntities1();
            UserProfileViewModel userView = new UserProfileViewModel();

            userView.regUserProfile = (from o in db.Registrations select o).ToList();
            userView.reqCycleUserProfile = (from or in db.RequestCycles select or).ToList();
            userView.cycDetailsUserProfile = (from or in db.CycleDetails select or).ToList();

            return View(userView);


            //Order order = db.Orders.Find(id);
            //return View(order);
        }
    }
}