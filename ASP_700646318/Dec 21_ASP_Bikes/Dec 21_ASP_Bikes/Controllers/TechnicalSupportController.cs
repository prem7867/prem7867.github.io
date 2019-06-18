using Dec_21_ASP_Bikes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace Dec_21_ASP_Bikes.Controllers
{
   
    public class TechnicalSupportController : Controller
    {

        private BikesEntities1 db = new BikesEntities1();


        // GET: SendEmail
        public ActionResult SendMailToAdmin()
            {
                return View();
            }

            [HttpPost]

            public ActionResult SendMailToAdmin(string ReceiverEmail, string Subject, string Message)
            {
                try
                {

                    if (ModelState.IsValid)
                    {

                        var senderEmail = new MailAddress("asp.a@mail.com", "Demo Test");
                        var receiverEmail = new MailAddress(ReceiverEmail, "Receiver");

                        var password = "Testing786";
                        var sub = Subject;
                        var body = Message;

                        var smtp = new SmtpClient
                        {
                            Host = "smtp.mail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };

                        using (var New_message = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = Subject,
                            Body = body
                        }
                        )
                        {
                            smtp.Send(New_message);
                            ViewBag.SuccessMail = "Email has been sent successfully.";


                        }

                        return View();
                    } //End Of If(ModelStste.IsValid)

                } //End of try

                catch (Exception)
                {
                    ViewBag.Error = "Email cannot be sent. Please check the error and try again";
                }

                return View();
            }







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
            // if (ModelState.IsValid)
            // {
            // db.RequestCycles.Add(rc);
            // db.SaveChanges();
            // return RedirectToAction("DisplayImage", "Image");
            //}

            var statusCycleId = db.RequestCycles.Where(s => s.Status == true).ToList();


            ViewBag.dropdownlist = new SelectList(statusCycleId, "CycleID", "CycleID");
            //ViewBag.currentDate = @DateTime.Now.Date; - testing
            return View();
        }

        [HttpPost]
        public ActionResult RequestCycleAdminInsertion(RequestCycle rc)
        {
            if (ModelState.IsValid)
            {


                db.RequestCycles.Add(rc);
                db.SaveChanges();



                return RedirectToAction("RequestCycleAdminInsertion", "TechnicalSupport");
            }
            //ViewBag.dropdownlist = new SelectList(db.CycleDetails, "CycleID", "CycleID");
            return View(rc);
        }



    }



}
    

