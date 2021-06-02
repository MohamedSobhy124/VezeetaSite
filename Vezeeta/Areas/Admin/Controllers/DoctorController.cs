using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;

namespace Vezeeta.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Admin/Doctors
        public ActionResult Index()
        {
            var doctors = db.Doctors.Include(a=>a.Insurance).Include(d => d.Admin).Include(d => d.Area).Include(d => d.City).Include(d => d.Specialty).Include(s=>s.DoctorPatients);

            return View(doctors.ToList());
        }
         public ActionResult Approve(int id)
        {
            Doctor doctor = db.Doctors.FirstOrDefault(a => a.DoctorID == id);
            doctor.DoctorStatus = AppointmentStatus.Approved;
            db.Entry(doctor).State = EntityState.Modified;
            var email = doctor.Email;
            var pass= doctor.PassWord;
            db.SaveChanges();
            if (doctor.DoctorStatus == AppointmentStatus.Approved)
            {
                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                MailAddress from = new MailAddress("VezeetaTeam1@gmail.com");
                StringBuilder sb = new StringBuilder();

                msg.From = new MailAddress("VezeetaTeam1@gmail.com");
                msg.To.Add(email);
                msg.Subject = "Wellcome in Vezeeta ";
                msg.IsBodyHtml = true;

                smtp.EnableSsl = true;


                smtp.Credentials = new System.Net.NetworkCredential(email, pass);

                sb.Append("Wellcome You Are Accepted In Vezeeta Doctors :" + "\n");
                sb.Append("Hi Doctor " + ":" + doctor.FullName + "\n");
                //sb.Append("Time of Booking" + ":" + appointment.Time.Hour + "\n");

                msg.Body = sb.ToString();
                smtp.Send(msg);
                msg.Dispose();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Deny(int id)
        {
            Doctor doctor = db.Doctors.FirstOrDefault(a => a.DoctorID == id);
            doctor.DoctorStatus = AppointmentStatus.Denied;
            db.Entry(doctor).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult searchDoctor(string seachText)
        {
            var Doctor = db.Doctors.Include(p => p.Admin).Include(p => p.DoctorPatients).Include(d => d.Area).Include(d => d.City).Include(d => d.Specialty); 
            var result = Doctor.Where(a => a.FName.ToLower().Contains(seachText) || a.LName.ToLower().Contains(seachText) || a.Phone.ToString().Contains(seachText) || a.Email.ToLower().Contains(seachText)).ToList();
            return PartialView("_GridDoctor", result);
        }

        // GET: Admin/Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Include(p => p.Admin).Include(p => p.DoctorPatients).Include(d => d.Area).Include(d => d.City).Include(d => d.Specialty).FirstOrDefault(a => a.DoctorID == id);
            //Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Admin/Doctors/Create
        public ActionResult Create()
        {
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty");
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");

            return View();
        }

        // POST: Admin/Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorID,FName,LName,PassWord,BirthDate,Gender,ExamineFee,Title,Image,SpecialtyID,PromoCode,WaitingTime,Phone,branchName,AreaID,CityID,AddressDetails,Email,Entity,IDImage,TitleImage,AdminID")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", doctor.AdminID);
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName", doctor.AreaID);
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", doctor.CityID);
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty", doctor.SpecialtyID);
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");

            return View(doctor);
        }

        // GET: Admin/Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", doctor.AdminID);
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName", doctor.AreaID);
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", doctor.CityID);
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty", doctor.SpecialtyID);
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");

            return View(doctor);
        }

        // POST: Admin/Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorID,FName,LName,PassWord,BirthDate,Gender,ExamineFee,Title,Image,SpecialtyID,PromoCode,WaitingTime,Phone,branchName,AreaID,CityID,AddressDetails,Email,Entity,IDImage,TitleImage,AdminID")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", doctor.AdminID);
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName", doctor.AreaID);
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", doctor.CityID);
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty", doctor.SpecialtyID);

            return View(doctor);
        }

        // GET: Admin/Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Doctor doctor = db.Doctors.Find(id);
            Doctor doctor = db.Doctors.Include(p => p.Admin).Include(p => p.DoctorPatients).Include(d => d.Area).Include(d => d.City).Include(d => d.Specialty).FirstOrDefault(a => a.DoctorID == id);

            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Admin/Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
