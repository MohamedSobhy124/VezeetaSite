using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;

namespace Vezeeta.Controllers
{
    public class AppointmentController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Appointment
        public ActionResult PatientAppointments()
        {
            var manager = new UserManager<MyIdentityUser>(new UserStore<MyIdentityUser>(new VezeetaIdentity()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var email = currentUser.Email;
            Patient patient = db.Patients.FirstOrDefault(p => p.Email == email);
            var appointments = db.Appointments.Where(a => a.PatientID == patient.PatientID).Include(a => a.Doctor).Include(a => a.Patient);
            return View(appointments.ToList());
        }



        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName");

            //ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name");

            //var viewModel = new AppointmentFormViewModel {Patient = User.Identity.GetUserId() };
            //var user =await UserManager.FindById(User.Identity.GetUserId());
            //var email = user.Email;
            //ViewBag.PatientID = User.Identity.GetUserId();
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorID,PatientID,Status,Date,Time")] Appointment appointment)
        {
            var manager = new UserManager<MyIdentityUser>(new UserStore<MyIdentityUser>(new VezeetaIdentity()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var email = currentUser.Email;
            Patient patient = db.Patients.FirstOrDefault(p => p.Email == email);
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    appointment.PatientID = patient.PatientID;
                    appointment.Status = AppointmentStatus.Pending;
                    db.Appointments.Add(appointment);
                    db.SaveChanges();
                    return RedirectToAction("PatientAppointments");
                }
                ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName", appointment.DoctorID);
                //ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", appointment.PatientID);
                return View(appointment);
            }
            return RedirectToAction("Login", "LogIn");

        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName", appointment.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", appointment.PatientID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorID,PatientID,Status,Date,Time")] Appointment appointment)
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

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
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