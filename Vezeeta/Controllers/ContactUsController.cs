using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;

namespace Vezeeta.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        MyModel db = new MyModel();

        public ActionResult ContactUs()
        {
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs(Contactus contactus)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Contactus.Add(contactus);
                    db.SaveChanges();
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    MailAddress from = new MailAddress(contactus.Email.ToString());
                    StringBuilder sb = new StringBuilder();

                    msg.From = new MailAddress(contactus.Email);
                    msg.To.Add("VezeetaTeam1@gmail.com");
                    msg.Subject = "Contact us";
                    msg.IsBodyHtml = true;

                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("VezeetaTeam1@gmail.com", "VezeetATeaM98");

                    sb.Append("Name" + ":" + contactus.Name + "\n");
                    sb.Append(Environment.NewLine);
                    sb.Append("Email" + ":" + contactus.Email + "\n");
                    sb.Append(Environment.NewLine);
                    sb.Append("Phone" + ":" + contactus.Phone + "\n");
                    sb.Append(Environment.NewLine);
                    sb.Append("Message" + ":" + contactus.Message + "\n");
                    sb.Append(Environment.NewLine);

                    msg.Body = sb.ToString();
                    smtp.Send(msg);
                    msg.Dispose();
                    ViewBag.Message = string.Format("Thanks For  {0} For Your Message  \\nat {1}", contactus.Name, DateTime.Now.ToString());

                    return View();

                }
                catch
                {
                    return View("Error");
                }
            }

            return View(contactus);
        }


    }
}
