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

    public class PatientsController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Admin/Patients
        public ActionResult Index()
        {
            var patients = db.Patients.Include(p => p.Admin).Include(p => p.doctor).Include(a=>a.PatientApointments);
            return View(patients.ToList());
        }
        public ActionResult Attend()
        {
          
            var pattient = db.Appointments.Where(a => a.IsAttend == AppointmentStatus.Denied).Include(a=>a.Patient);
            //.GroupBy(a=>a.PatientID).Select(s=> new {})
            //.Where(a=>a.IsAttend==AppointmentStatus.Denied).GroupBy(a=>a.IsAttend);
            return View(pattient.ToList());
        }


        public PartialViewResult searchPatient(string seachText)
        {
            var patients = db.Patients.Include(p => p.Admin).Include(p => p.doctor);
            var result = patients.Where(a => a.Name.ToLower().Contains(seachText) || a.Phone.ToString().Contains(seachText) || a.Email.ToLower().Contains(seachText)).ToList();
            return PartialView("_GridPatient", result);
        }

        // GET: Admin/Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Patient patient = db.Patients.Find(id);
            Patient patient = db.Patients.Include(p => p.Admin).Include(p => p.doctor).Include(d => d.PatientRatings).Include(d => d.PatientApointments).FirstOrDefault(a => a.PatientID == id);

            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Admin/Patients/Create
        public ActionResult Create()
        {
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name");
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName");
            return View();
        }

        // POST: Admin/Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientID,Name,PassWord,Gender,Address,BirthDate,Phone,Email,AdminID,DoctorID")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", patient.AdminID);
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName", patient.DoctorID);
            return View(patient);
        }

        // GET: Admin/Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", patient.AdminID);
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName", patient.DoctorID);
            return View(patient);
        }

        // POST: Admin/Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientID,Name,PassWord,Gender,Address,BirthDate,Phone,Email,AdminID,DoctorID")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", patient.AdminID);
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName", patient.DoctorID);
            return View(patient);
        }

        // GET: Admin/Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        public ActionResult Delete2(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.FirstOrDefault(a=>a.Name==id);
            db.Patients.Remove(patient);
            db.SaveChanges();

            if (patient == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Attend");
        }
        // POST: Admin/Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
