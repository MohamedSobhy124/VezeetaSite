using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;

namespace Vezeeta.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AppointmentsController : Controller
    {
        private MyModel db = new MyModel();

        private readonly UserManager<MyIdentityUser> userManager;
        //private readonly PatientService patientService; 
        public AppointmentsController()
        {
            var db = new VezeetaIdentity();
            var UserStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(UserStore);
        }
        // GET: Admin/Appointments
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.Doctor).Include(a => a.Patient);
            return View(appointments.ToList());
        }

        public PartialViewResult searchAppointment(string seachText)
        {
            var appointment = db.Appointments.Include(p => p.Doctor).Include(p => p.Patient);
            var result = appointment.Where(a => a.Date.ToString().ToLower().Contains(seachText) || a.Time.ToString().Contains(seachText) || a.Doctor.FName.ToLower().Contains(seachText) || a.Doctor.LName.ToLower().Contains(seachText) || a.Patient.Name.ToLower().Contains(seachText)).ToList();
            return PartialView("_GirdAppointment", result);
        }

     
        public ActionResult Details(int? did)
        {
            if (did == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             Appointment appointment = db.Appointments.Include(a=>a.Doctor).Include(a=>a.Patient).FirstOrDefault(a=>a.DoctorID==did);


            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }
        // GET: Admin/Appointments/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName");
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name");
            return View();
        }

        // POST: Admin/Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorID,PatientID,Date,Time")] Appointment appointment)
        {
            //var x= userManager.FindById("aa")
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName", appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", appointment.PatientID);
            return View(appointment);
        }

        // GET: Admin/Appointments/Edit/5
        public ActionResult Edit(int? did,Appointment app)
        {
            if (did == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(did );
            appointment.Date = app.Date;
            appointment.Time = app.Time;
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName", appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", appointment.PatientID);
            return View(appointment);
        }

        // POST: Admin/Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorID,PatientID,Date,Time")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName", appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", appointment.PatientID);
            return View(appointment);
        }

        // GET: Admin/Appointments/Delete/5
        public ActionResult Delete(int? did)
        {
            if (did == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Appointment appointment = db.Appointments.Find(did, pid);
            Appointment appointment = db.Appointments.Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefault(a => a.DoctorID == did);

            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Admin/Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? did)
        {
            Appointment appointment = db.Appointments.Find(did);
            db.Appointments.Remove(appointment);
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
