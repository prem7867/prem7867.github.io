using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Dec_21_ASP_Bikes.Controllers
{

  
    public class SendEmailController : Controller
    {
        // GET: SendEmail
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Form(string ReceiverEmail, string Subject, string Message)
        {
            try
            {

                if (ModelState.IsValid)
                {

                  var senderEmail = new MailAddress("asp.a@mail.com", "UCM Bikes");
                    var receiverEmail = new MailAddress(ReceiverEmail, "Receiver");

                    var password = "Testing786";
                    var sub = "Reminder from UCM Bikes";
                    var body = "Hello... Your time to return the cycle has exceeded";

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
                        Subject = sub,
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
    }
}

