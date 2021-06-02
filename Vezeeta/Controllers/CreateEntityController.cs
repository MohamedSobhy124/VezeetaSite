using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;

namespace Vezeeta.Controllers
{
    public class CreateEntityController : Controller
    {

        MyModel db = new MyModel();


        // GET: CreateEntity
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult createentity()
        {
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createentity([Bind(Include = "Entity,EntityName,TitleImage")] Doctor doctor, HttpPostedFileBase EnityImg)
        {
            if (ModelState.IsValid)
            {
                if (db.Doctors.Any(d => d.Entity == doctor.Entity))
                {
                    ModelState.AddModelError("", "Entity already exists.");
                }
                else
                {
                    db.Doctors.Add(doctor);
                    db.SaveChanges();

                    String EntityFileName = doctor.DoctorID.ToString() + "." + EnityImg.FileName.Split('.')[1];
                    EnityImg.SaveAs(Server.MapPath("~/images/") + EntityFileName);
                   // doctor.TitleImage = EntityFileName;
                    db.SaveChanges();

                    return RedirectToAction("setupVeezete", "SetupVeezeteBook");
                }

            }

            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", doctor.AdminID);
            return View(doctor);
        }
    }
}