using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Controllers
{
    public class PasswordController : Controller
    {
        // GET: Password
        public ActionResult Index()
        {
            return View();
        }

        //Reset Password Link

        public ActionResult PasswordRest()
        {

            return View();
        }
    }
}