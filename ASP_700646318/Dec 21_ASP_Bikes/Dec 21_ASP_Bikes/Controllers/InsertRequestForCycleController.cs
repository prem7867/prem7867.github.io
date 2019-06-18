using Dec_21_ASP_Bikes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Controllers
{
    public class InsertRequestForCycleController : Controller
    {
      /* [HttpPost]
        public ActionResult AddToRequestTable(CycleRequestedByUser movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Unchanged;
                db.SaveChanges();
                return RedirectToAction("DisplayImage", "Image");
            }
            return View(movie);
        }
		

        
                // GET: InsertRequestForCycle
               public ActionResult InsertRequestByAdmin()
                {
                    return View();
                }


              [HttpPost]
                public ActionResult InsertRequestByAdmin(RequestCycle rc)
                {

                        BikesEntities db = new BikesEntities();

                    RequestCycleDetailsViewModel cid = new RequestCycleDetailsViewModel();
                    ////CycleDetail cid = new CycleDetail();
                         cid.CycleDetail = (from s in db.CycleDetails
                                           select new SelectListItem()
                                           {
                                               Value = s.CycleAccessories,
                                               Text = SqlFunctions.StringConvert((double)s.CycleID)
                                           }).ToList<SelectListItem>();
                   db.RequestCycles.Add(rc);
                   db.SaveChanges();


                    return View(cid);
                }

		*/
                


        private BikesEntities1 db = new BikesEntities1();
        public ActionResult RequestCyclget()
        {
            BikesEntities1 db = new BikesEntities1();
            ViewBag.QualList = new SelectList(db.CycleDetails, "CycleID", "CycleType");
            return View();
        }
        // POST: /Home/CreateEmployee
        // [HttpPost, ActionName("Request rc")]


        public ActionResult RequestCycleAdminInsertion()
        {
            if (ModelState.IsValid)
            {
                // {
                // db.RequestCycles.Add(rc);
                // db.SaveChanges();
                // return RedirectToAction("DisplayImage", "Image");
                //}
                ViewBag.dropdownlist = new SelectList((from r in db.CycleDetails
                                                       where !(from u in db.RequestCycles
                                                               select u.CycleID).Contains(r.CycleID)
                                                       select r).Distinct()

                    , "CycleID", "CycleID");

                var cycleIDList = new SelectList((from r in db.CycleDetails
                                                  where !(from u in db.RequestCycles
                                                          select u.CycleID).Contains(r.CycleID)
                                                  select r).Distinct()
                                                   , "", "CycleID");
                //ViewBag.currentDate = @DateTime.Now.Date; - testing

                if (cycleIDList.ElementAtOrDefault(0)==null)
                {
                    ViewBag.CycleIDError = "Sorry. Currently there are no Cycles available to raise a request.";

                }



                else
                {
                    return View();

                }
            }

                return View();
            
        }


		[HttpPost]
		public ActionResult RequestCycleAdminInsertion(RequestCycle rc)
		{
			if (ModelState.IsValid)
			 {

                //if (rc.ToDate != null)
                //{
                //    rc.Status = true;
                //}

                //else
                //{
                //    rc.Status = false;
                //}
                //var cycleId = db.CycleRequestedByUsers.Where(a => a.Username == User.Identity.Name).Select(a=>a.CycleID);


              if (rc.CycleID.Equals(null))
                {
                    ViewBag.CycleIDError = "Sorry. Currently there are no Cycles available to raise a request.";
                }
                else
                {

                    db.RequestCycles.Add(rc);
                    db.SaveChanges();



                    return RedirectToAction("CycleList", "CycleDetailsList");

                }// return RedirectToAction("RequestCycleAdminInsertion", "InsertRequestForCycle");
            }
            //ViewBag.dropdownlist = new SelectList(db.CycleDetails, "CycleID", "CycleID");
            return View(rc);
		}

        

    }
}