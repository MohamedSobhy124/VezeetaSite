namespace Vezeeta.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Web.Mvc;
    using Vezeeta.Models;
    using Vezeeta.ViewModel;

    public class DoctorsController : Controller
    {

        private MyModel db = new MyModel();

        public ActionResult HomePage()
        {
            var doctor = db.Doctors.Include(a=>a.Insurance).Include(a => a.Area).Include(a => a.City).Include(a => a.Specialty);
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpeciltyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty");
            return View(doctor.ToList());
        }

        public ActionResult City2()
        {
            ViewBag.area = db.Doctors.Select(a => a.Area.AreaName).ToList();

            var doc = db.Cities.Include(a => a.Areas).ToList();
            List<Area> MasterData = db.Areas.ToList();



            ViewBag.Ares = new SelectList(db.Areas, "AreaID", "AreaName").ToList();
            return View(doc);
        }
        public ActionResult specific2()
        {
            var spec = db.Specialties.ToList();
            return View(spec);
        }
        public ActionResult Details()
        {
            var doctors = db.Doctors.Where(a=>a.DoctorStatus==AppointmentStatus.Approved).Include(d => d.Admin).Include(a => a.City).Include(a => a.Area).Include(s => s.Specialty);
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");
            ViewBag.AreaID = new SelectList(db.Areas.ToList(), "AreaID", "AreaName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpeciltyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty");

            return View(doctors.ToList());
        }

        public ActionResult search(string docname = null, string sps = null)
        {
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpeciltyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty", sps);
            if (docname != null)
            {
                List<Doctor> doc = db.Doctors
                        .Include(a => a.City)
                        .Include(a => a.Area)
                        .Include(a => a.Specialty)
                        .Where(d => d.FName.ToLower().Contains(docname) || d.LName.ToLower().Contains(docname) || d.Insurance.CompanyName.ToLower().Contains(docname) || d.Specialty.Specilty.ToLower().Contains(docname) || d.City.CityName.ToLower().Contains(docname) || d.Area.AreaName.ToLower().Contains(docname)).ToList();
                ViewBag.drCount = doc.Count();
                return View("Details", doc);
            }
            else
                return View("Details");
        }
            public ActionResult search2(int? InsuranceID, int? SpeciltyID, int? AreaID, int? CityID, string sps = null)
        {
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpeciltyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty", sps);
            List<Doctor> doc = db.Doctors.Where(d =>d.DoctorStatus==AppointmentStatus.Approved  && d.City.CityID == (CityID == null ? d.City.CityID : CityID) && d.Specialty.SpecialtyID == (SpeciltyID == null ? d.Specialty.SpecialtyID : SpeciltyID) && d.Area.AreaID == (AreaID == null ? d.Area.AreaID : AreaID) && d.Insurance.InsuranceID == (InsuranceID == null ? d.Insurance.InsuranceID : InsuranceID) )
                .Include(a => a.City).Include(a=>a.Insurance).Include(a => a.Area)
                .Include(a => a.Specialty).ToList();
            ViewBag.drCount = doc.Count();

            return View("Details", doc);
        }

        public ActionResult search3(int? SpeciltyID)
        {
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpeciltyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty",SpeciltyID);
            List<Doctor> doc = db.Doctors.Where(a =>a.DoctorStatus == AppointmentStatus.Approved && a.SpecialtyID == SpeciltyID).ToList();
            return View("Details", doc);

        }
        public ActionResult search4(string Entity=null )
        {
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpeciltyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty", Entity);
            List<Doctor> doc = db.Doctors.Where(a =>a.DoctorStatus == AppointmentStatus.Approved && a.branchName==Entity).ToList();
            return View("Details", doc);

        }
        public ActionResult search5(int? CityID)
        {
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", CityID);
            ViewBag.SpeciltyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty");
            List<Doctor> doc = db.Doctors.Where(a => a.CityID == CityID).ToList();
            return View("Details", doc);

        }
        public ActionResult search6(int? InsuranceID)
        {
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpeciltyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty");
            List<Doctor> doc = db.Doctors.Where(a => a.InsuranceID == InsuranceID).ToList();
            return View("Details", doc);

        }
        public JsonResult GetStateById(int CityID)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return Json(db.Areas.Where(a => a.CityID == CityID), JsonRequestBehavior.AllowGet);
        }
        //Doctor Appointments
        public ActionResult DoctorAppointments()
        {
            var manager = new UserManager<MyIdentityUser>(new UserStore<MyIdentityUser>(new VezeetaIdentity()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var email = currentUser.Email;
            Doctor doctor = db.Doctors.FirstOrDefault(d => d.Email == email);
            var doctorDetail = new DoctorDetailViewModel();
            doctorDetail.Appointments = db.Appointments.Where(d => d.Doctor.DoctorID == doctor.DoctorID && d.Date >= DateTime.Today).Include(p => p.Patient).OrderBy(d => d.Date).ToList();
            doctorDetail.Doctor = doctor;
            return View(doctorDetail);
        }
        public ActionResult TodayAppointments()
        {
            var manager = new UserManager<MyIdentityUser>(new UserStore<MyIdentityUser>(new VezeetaIdentity()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var email = currentUser.Email;
            Doctor doctor = db.Doctors.FirstOrDefault(d => d.Email == email);
            var doctorDetail = new DoctorDetailViewModel();
            doctorDetail.Appointments = db.Appointments.Where(d => d.Doctor.DoctorID == doctor.DoctorID && d.Date == DateTime.Today).Include(p => p.Patient).OrderBy(d => d.Date).ToList();
            doctorDetail.Doctor = doctor;
            return View(doctorDetail);
        }
        public ActionResult Approve(int id)
        {
            Appointment appointment = db.Appointments.FirstOrDefault(a => a.AppointmentID == id);
            Patient p = db.Patients.FirstOrDefault(x => x.PatientID == appointment.PatientID);
            appointment.Status = AppointmentStatus.Approved;
            db.Entry(appointment).State = EntityState.Modified;
            var email = p.Email;
            var pass = p.PassWord;
            db.SaveChanges();
            if (appointment.Status == AppointmentStatus.Approved)
            {
                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                MailAddress from = new MailAddress("VezeetaTeam1@gmail.com");
                StringBuilder sb = new StringBuilder();
                msg.From = new MailAddress("VezeetaTeam1@gmail.com");
                msg.To.Add(email);
                msg.Subject = "Appointment Booking";
                msg.IsBodyHtml = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(email, pass);
                sb.Append("Appointment Booking  is approved at :" + "\n");
                sb.Append("Date of Booking" + ":" + appointment.Date.Day + "\n");
                sb.Append("Time of Booking" + ":" + appointment.Time.Hour + "\n");
                msg.Body = sb.ToString();
                smtp.Send(msg);
                msg.Dispose();
            }
            return RedirectToAction("DoctorAppointments");
        }
        public ActionResult Approve2(int id)
        {
            Appointment appointment = db.Appointments.FirstOrDefault(a => a.AppointmentID == id);
            Patient p = db.Patients.FirstOrDefault(x => x.PatientID == appointment.PatientID);
            appointment.IsAttend = AppointmentStatus.Approved;
            db.Entry(appointment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DoctorAppointments");
        }
        public ActionResult Deny(int id)
        {
            Appointment appointment = db.Appointments.FirstOrDefault(a => a.AppointmentID == id);
            appointment.Status = AppointmentStatus.Denied;
            db.Entry(appointment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DoctorAppointments");
        }
        public ActionResult Deny2(int id)
        {
            Appointment appointment = db.Appointments.FirstOrDefault(a => a.AppointmentID == id);
            appointment.IsAttend = AppointmentStatus.Denied;
            db.Entry(appointment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DoctorAppointments");
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
