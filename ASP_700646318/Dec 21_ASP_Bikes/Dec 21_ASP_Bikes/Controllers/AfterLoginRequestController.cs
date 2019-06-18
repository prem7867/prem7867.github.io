using Dec_21_ASP_Bikes.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Controllers
{
    public class AfterLoginRequestController : Controller
    {
        private BikesEntities1 db = new BikesEntities1();
        // GET: AfterLoginRequest
        public ActionResult Index()
        {
            return View();
        }


        // GET: CycleDetailsList
        public ActionResult CycleList(string searchBy, string search, int? page)
        {
            List<CycleDetail> allCycles = new List<CycleDetail>();
            allCycles = db.CycleDetails.ToList();

            if (searchBy == "CycleAccessories")
            {

                //  return View(db.CycleDetails.Where(x => x.CycleAccessories.StartsWith(search) || search == null).OrderByDescending(u=>u.CycleID).ToList().ToPagedList(page ?? 1, 3));
                var list = (from r in db.CycleDetails.Where(x => x.CycleAccessories.StartsWith(search) || search == null)
                            where !(from u in db.CycleRequestedByUsers
                                    select u.CycleID).Contains(r.CycleID)
                            select r).Distinct().
                           OrderByDescending(u => u.CycleID).ToList();//ToPagedList(page ?? 1, 3);
                return View(list.ToList().ToPagedList(page ?? 1, 7));
            }
            else
            {
                //return View(db.CycleDetails.Where(x => x.CycleType.StartsWith(search) || search == null).OrderByDescending(u => u.CycleID).ToList().ToPagedList(page ?? 1, 3));
                var list = (from r in db.CycleDetails.Where(x => x.CycleType.StartsWith(search) || search == null)
                            where !(from u in db.CycleRequestedByUsers
                                    select u.CycleID).Contains(r.CycleID)
                            select r).Distinct().
                           OrderByDescending(u => u.CycleID).ToList();// ToPagedList(page ?? 1, 3);
                return View(list.ToList().ToPagedList(page ?? 1, 7));
            }

        }



        public ActionResult CycleAlreadyPresentList(string searchBy, string search, int? page)
        {
            List<CycleDetail> allCycles = new List<CycleDetail>();
            allCycles = db.CycleDetails.ToList();

            if (searchBy == "CycleAccessories")
            {

                //return View(db.CycleDetails.Where(x => x.CycleAccessories.StartsWith(search) || search == null).OrderByDescending(u => u.CycleID).ToList().ToPagedList(page ?? 1, 3));

                var list = (from r in db.CycleDetails.Where(x => x.CycleAccessories.StartsWith(search) || search == null)
                            where !(from u in db.CycleRequestedByUsers
                                    select u.CycleID).Contains(r.CycleID)
                            select r).Distinct().
                            OrderByDescending(u => u.CycleID).ToList();//ToPagedList(page ?? 1, 3);
                return View(list.ToList().ToPagedList(page ?? 1, 7));
            }
            else
            {
                //return View(db.CycleDetails.Where(x => x.CycleType.StartsWith(search) || search == null).OrderByDescending(u => u.CycleID).ToList().ToPagedList(page ?? 1, 3));

                var list = (from r in db.CycleDetails.Where(x => x.CycleType.StartsWith(search) || search == null)
                            where !(from u in db.CycleRequestedByUsers
                                    select u.CycleID).Contains(r.CycleID)
                            select r).Distinct().
                            OrderByDescending(u => u.CycleID).ToList();//ToPagedList(page ?? 1, 3);
                return View(list.ToList().ToPagedList(page ?? 1, 7));

                //(from r in db.RequestCycles
                // where !(from u in db.CycleRequestedByUsers
                //         select u.RequestID).Contains(r.RequestID)
                // select r).Distinct().Where(r => r.CycleID == id);
                //return View(query.ToList().ToPagedList(page ?? 1, 7));
            }

        }




    }
}