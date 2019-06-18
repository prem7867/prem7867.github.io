using Dec_21_ASP_Bikes.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Controllers
{
   
    public class ReportController : Controller
    {
         
        public ActionResult Index()
        {
            ViewBag.Message = "Please display the student details";
            return View();
        }
        //Report

        
        public ActionResult Report_Registration()
		/*{
            using (BikesEntities dc = new BikesEntities())
            {
                var v = dc.Registrations.ToList();
                return View(v);
            }
        }*/

		{
			List<Registration> allStudents = new List<Registration>();

			// Here BikeEntities is our Data Context
			using (BikesEntities1 db = new BikesEntities1())
			{
				allStudents = db.Registrations.ToList();
			}
			return View(allStudents);
		}

		//Print as Documents

		public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "Registration.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<Registration> cm = new List<Registration>();
            using (BikesEntities1 dc = new BikesEntities1())
            {
                cm = dc.Registrations.ToList();
            }
            ReportDataSource rd = new ReportDataSource("RegisteredStudentList", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }


        //public ActionResult DeleteStudents(string[] ids)
        //{
        //	//Delete Selected 
        //	long [] id = null;
        //	if (ids != null)
        //	{
        //		id = new long[ids.Length];
        //		int j = 0;
        //		foreach (string i in ids)
        //		{
        //			long.TryParse(i, out id[j++]);
        //		}
        //	}

        //	if (id != null && id.Length > 0)
        //	{
        //		List<Registration> allSelected = new List<Registration>();
        //		using (BikesEntities1 db = new BikesEntities1())
        //		{
        //			allSelected = db.Registrations.Where(a => id.Contains(a.StudentID)).ToList();
        //			foreach (var i in allSelected)
        //			{
        //				db.Registrations.Remove(i);
        //			}
        //			db.SaveChanges();
        //		}
        //	}
        //	return RedirectToAction("Report_Registration");
        //}




        ////Disabling Students

        //public ActionResult DisableStudents()
        //{
        //      BikesEntities1 db = new BikesEntities1();

        //    var check1StudentExits = db.Registrations.Where(a => a.Username == User.Identity.Name).Take(1).Single().Username;

        //    var check2StatusInTable = db.CycleRequestedByUsers.Where(a => a.Username == d).OrderByDescending(x => x.UserRequest).Take(1).Single().Status;


        //    if (check1UserExits.Equals(User.Identity.Name) && check2StatusInTable.Equals(true))
        //    {
        //        //return RedirectToAction("RestrictUserFromRequestingAnotherCycle", "Test");
        //        //return Content("Sorry. You are not eligible since you already have a cyle with you");

        //        ViewBag.UserALreadyHasBorrowed = "Sorry " + User.Identity.Name + "😰😰😰😰." + " You are not eligible since you already have a 🚲 with you";

        //        ViewBag.link = "👉👉👉👉";

        //        //return Content("Congratz. You are eligible since you have returned the previous one");
        //        return View();


        //    }
        //    return View();

        //}




        //Report Generation for the Bicycles borrowed

        public ActionResult Report_CyclesBorrowed()
       
        {
            List<CycleRequestedByUser> allCyclesBorrowed = new List<CycleRequestedByUser>();

            // Here BikeEntities is our Data Context
            using (BikesEntities1 db = new BikesEntities1())
            {
                allCyclesBorrowed = db.CycleRequestedByUsers.ToList();
            }
            return View(allCyclesBorrowed);
        }

        public ActionResult CyclesReport(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reports"), "Cycles.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<CycleRequestedByUser> cm = new List<CycleRequestedByUser>();
            using (BikesEntities1 dc = new BikesEntities1())
            {
                cm = dc.CycleRequestedByUsers.ToList();
            }
            ReportDataSource rd = new ReportDataSource("CyclesDataList", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }



    }
}

