using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dec_21_ASP_Bikes.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using PagedList;

namespace Dec_21_ASP_Bikes.Controllers
{

    public class RequestCycleController : Controller
    {

        // GET: Cycle
        public ActionResult Index()
        {
            BikesEntities1 db = new BikesEntities1();
            List<CycleDetail> clist = db.CycleDetails.ToList();
            return View(clist);
        }


        //        public ActionResult RequestName(int id)
        //        {
        //            BikesEntities1 db = new BikesEntities1();
        //            // List<RequestCycle> crequest = db.RequestCycles.Where(s => s.CycleID == id).ToList();
        //            // List<RequestCycle> crequest = db.RequestCycles.Include(c=> c.CycleRequestedByUsers).Distinct().Where(s => s.CycleID == id).ToList();
        //            List<RequestCycle> crequest = db.RequestCycles.Where(s => s.CycleID == id).ToList();


        //            var query = (from r in db.RequestCycles
        //                         where !(from u in db.CycleRequestedByUsers
        //                                 select u.RequestID).Contains(r.RequestID)
        //                         select r).Distinct();
        //           // var rid = query.Select(q => q.RequestID == cid);
        //            return View();

        //}



        public ActionResult RequestName(int id, int? page)
        {
            BikesEntities1 db = new BikesEntities1();
            // List<RequestCycle> crequest = db.RequestCycles.Where(s => s.CycleID == id).ToList();
            // List<RequestCycle> crequest = db.RequestCycles.Include(c=> c.CycleRequestedByUsers).Distinct().Where(s => s.CycleID == id).ToList();
           // List<RequestCycle> crequest = db.RequestCycles.Where(s => s.RequestID == id).ToList();


            //var query = (from r in db.RequestCycles
            //             where !(from u in db.CycleRequestedByUsers
            //                     select u.RequestID).Contains(r.RequestID)
            //             select r).Distinct();
            //return View(query.ToList().ToPagedList(page ?? 1, 7));

            var query = (from r in db.RequestCycles
                         where !(from u in db.CycleRequestedByUsers
                                 select u.RequestID).Contains(r.RequestID)
                         select r).Distinct().Where(r=>r.CycleID == id);
            return View(query.ToList().ToPagedList(page ?? 1, 7));


        }




        //ViewBag.query = (from r in crequest
        //             where !(from u in db.CycleRequestedByUsers
        //                     select u.RequestID).Contains(r.RequestID)
        //             select r).Distinct();
        //if(ViewBag.query == null)
        //{
        //    return Content("Sorry. This 🚲 is currently ❌ available. Please cum back later or try another 🚲");

        //}
        //else
        //{
        //       return View(ViewBag.query);




        [HttpPost]
        public ActionResult RequestName(CycleRequestedByUser rbu)
        {
           // try
            //{
                BikesEntities1 db = new BikesEntities1();
               // List<RequestCycle> rc = db.rc.ToList();
              
                //////int rid = rbu.RequestID;
               // int cid = rbu.CycleID;
               // DateTime fd = rbu.FromDate;
              //  DateTime td = rbu.ToDate;

               // CycleRequestedByUser crbu = new CycleRequestedByUser();
               // crbu.RequestID = rbu.RequestID;
                //crbu.CycleID = rbu.CycleID;
                //crbu.FromDate = (DateTime)rbu.FromDate;
              //  crbu.ToDate = (DateTime)rbu.ToDate;

               //select and insert statement

                db.CycleRequestedByUsers.Add(rbu);
                db.SaveChanges();
          //  }
            //catch (Exception ex)
          //  {
                //throw ex;
            

            //int RequestedCycleFromViewID = crbu.RequestID;

            return View(rbu);
        }


      



        //HttpGet





        //Insert Cyclist that could be requested

        public ActionResult RequestCycle()

    {
        CycleDetail c1 = new CycleDetail();
        return View(c1);
    }


    [HttpPost]

    public ActionResult RequestCycle(CycleDetail model, HttpPostedFileBase image1)
    {
        //creating an object of the context class to save the file into database table.
        var db = new BikesEntities1();

        //check if the image1 (id or name) is not equal to null
        if (image1 != null)
        {
            //convert your image into binary format;

            model.CycleImage = new byte[image1.ContentLength];

            //inputstream is used to convert the actual data to binary format
            image1.InputStream.Read(model.CycleImage, 0, image1.ContentLength);


        }
        //save model into datacontext
        db.CycleDetails.Add(model);
        db.SaveChanges();
        return View(model);
    }

}
    }
