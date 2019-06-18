using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dec_21_ASP_Bikes.Models;
using PagedList;
using PagedList.Mvc;

namespace Dec_21_ASP_Bikes.Controllers
{
    public class CycleDetailsListController : Controller
    {
        //here  BikeEntities is the dbcontext
        private BikesEntities1 db = new BikesEntities1();

       
        // GET: CycleDetailsList
        public ActionResult CycleList(string searchBy, string search, int? page)
        {
            List<CycleDetail> allCycles = new List<CycleDetail>();
            allCycles = db.CycleDetails.ToList();

            var cyclisList = (from r in db.CycleDetails
                              where !(from u in db.CycleRequestedByUsers
                                      select u.CycleID).Contains(r.CycleID)
                              select r).Distinct().
                                            OrderByDescending(u => u.CycleID).ToList();// ToPagedList(page ?? 1, 3);
            if (cyclisList.Count==0 || cyclisList.Equals(null))

            {
                ViewBag.NoCyclesavailable = "Sorry " + User.Identity.Name + "😰😰😰😰." + " Currently, there are no 🚲's available to borrow";

            }
            else if (searchBy == "CycleAccessories")
                {

                //  return View(db.CycleDetails.Where(x => x.CycleAccessories.StartsWith(search) || search == null).OrderByDescending(c=>c.CycleAccessories).ToList().ToPagedList(page ?? 1, 3));
                var list = (from r in db.CycleDetails.Where(x => x.CycleAccessories.StartsWith(search) || search == null)
                            where !(from u in db.CycleRequestedByUsers
                                    select u.CycleID).Contains(r.CycleID)
                            select r).Distinct().
                                            OrderByDescending(u => u.CycleID).ToList();// ToPagedList(page ?? 1, 3);
                    return View(list.ToList().ToPagedList(page ?? 1, 7));

                }
                else
                {
                //return View(db.CycleDetails.Where(x => x.CycleType.StartsWith(search) || search == null).OrderByDescending(c=>c.CycleID).ToList().ToPagedList(page ?? 1, 3));

                var list = (from r in db.CycleDetails.Where(x => x.CycleType.StartsWith(search) || search == null)
                            where !(from u in db.CycleRequestedByUsers
                                    select u.CycleID).Contains(r.CycleID)
                            select r).Distinct().
                                OrderByDescending(u => u.CycleID).ToList();// ToPagedList(page ?? 1, 3);
                    return View(list.ToList().ToPagedList(page ?? 1, 7));
                }
            
            return View();

        }


        public ActionResult CycleListWithOnlyViewOption(string searchBy, string search, int? page)
        {
            List<CycleDetail> allCycles = new List<CycleDetail>();
            allCycles = db.CycleDetails.ToList();

            if (searchBy == "CycleAccessories")
            {

                //return View(db.CycleDetails.Where(x => x.CycleAccessories.StartsWith(search) || search == null).OrderByDescending(c=>c.CycleID).ToList().ToPagedList(page ?? 1, 3));
                var list = (from r in db.CycleDetails.Where(x => x.CycleAccessories.StartsWith(search) || search == null)
                            where !(from u in db.CycleRequestedByUsers
                                    select u.CycleID).Contains(r.CycleID)
                            select r).Distinct().
                                                  OrderByDescending(u => u.CycleID).ToList();// ToPagedList(page ?? 1, 3);
                return View(list.ToList().ToPagedList(page ?? 1, 7));
            }
        
            else
            {
                // return View(db.CycleDetails.Where(x => x.CycleType.StartsWith(search) || search == null).OrderByDescending(c=>c.CycleID).ToList().ToPagedList(page ?? 1, 3));
                var list = (from r in db.CycleDetails.Where(x => x.CycleType.StartsWith(search) || search == null)
                            where !(from u in db.CycleRequestedByUsers
                                    select u.CycleID).Contains(r.CycleID)
                            select r).Distinct().
                                        OrderByDescending(u => u.CycleID);// ToList().ToPagedList(page ?? 1, 3);
                return View(list.ToList().ToPagedList(page ?? 1, 7));
            }

        }








        //Delete Cycle Details from Web Grid

        public ActionResult DeleteList()
        {
            List<CycleDetail> allCycles = new List<CycleDetail>();

            // Here BikeEntities is our Data Context
            using (BikesEntities1 db = new BikesEntities1())
            {
                allCycles = db.CycleDetails.ToList();
            }
            return View(allCycles);
        }

        
        //For Delete method
        public ActionResult DeleteSelected(string[] ids)
        {
            //Delete Selected 
            int[] id = null;
            if (ids != null)
            {
                id = new int[ids.Length];
                int j = 0;
                foreach (string i in ids)
                {
                    int.TryParse(i, out id[j++]);
                }
            }

            if (id != null && id.Length > 0)
            {
                List<CycleDetail> allSelected = new List<CycleDetail>();
                using (BikesEntities1 db = new BikesEntities1())
                {
                    allSelected = db.CycleDetails.Where(a => id.Contains(a.CycleID)).ToList();
                    foreach (var i in allSelected)
                    {
                        db.CycleDetails.Remove(i);
                    }
                    db.SaveChanges();
                }
            }
            return RedirectToAction("DeleteList");
        }


    }
}