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

    public class RatingsController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Admin/Ratings
        public ActionResult Index()
        {
            var ratings = db.Ratings.Include(r => r.doctor).Include(r => r.patient);
            return View(ratings.ToList());
        }

        public PartialViewResult searchAppointment(string seachText)
        {
            var appointment = db.Ratings.Include(p => p.doctor).Include(p => p.patient);
            var result = appointment.Where(a => a.patient.Name.ToLower().Contains(seachText) || a.TotalRating.ToString().Contains(seachText) || a.doctor.FName.ToLower().Contains(seachText) || a.doctor.LName.ToLower().Contains(seachText)).ToList();
            return PartialView("_GridRating", result);
        }
        // GET: Admin/Ratings/Details/5
        public ActionResult Details(int? did, int? pid)
        {
            if (did == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Include(a => a.doctor).Include(a => a.patient).FirstOrDefault(a => a.DoctorID == did && a.PatientID == pid);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Admin/Ratings/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName");
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name");
            return View();
        }

        // POST: Admin/Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorID,PatientID,EntityRating,AssistantRating,TotalRating")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName", rating.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", rating.PatientID);
            return View(rating);
        }

        // GET: Admin/Ratings/Edit/5
        public ActionResult Edit(int? did, int? pid)
        {
            if (did == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(did, pid);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName", rating.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", rating.PatientID);
            return View(rating);
        }

        // POST: Admin/Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorID,PatientID,EntityRating,AssistantRating,TotalRating")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FullName", rating.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", rating.PatientID);
            return View(rating);
        }

        // GET: Admin/Ratings/Delete/5
        public ActionResult Delete(int? did, int? pid)
        {
            if (did == null || pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Include(a => a.doctor).Include(a => a.patient).FirstOrDefault(a => a.DoctorID == did && a.PatientID == pid);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Admin/Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? did, int? pid)
        {
            Rating rating = db.Ratings.Find(did, pid);
            db.Ratings.Remove(rating);
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
