using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dec_21_ASP_Bikes.Models;
using System.IO;
using System.Web.Security;
using Dec_21_ASP_Bikes.ViewModel;
using System.Data.Entity;







namespace Dec_21_ASP_Bikes.Controllers
{
   
    
    public class ImageController : Controller
    {
        // GET: Image

    
        public ActionResult AddImage()

        {
            CycleDetail c1 = new CycleDetail();
            return View(c1);
        }

        
        [HttpPost]

        public ActionResult AddImage(CycleDetail model, HttpPostedFileBase image1)
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
            ModelState.Clear();
            ViewBag.InsertedImageDetails = "Cycle Details were inserted successfully";


            return View(model);

            }



        //Display Image


        public ActionResult DisplayImage()
        {
            BikesEntities1 db = new BikesEntities1();
            var imageList = (from d in db.CycleDetails
                             select d).ToList();

            return View(imageList);

        }

    }

        }
    

        