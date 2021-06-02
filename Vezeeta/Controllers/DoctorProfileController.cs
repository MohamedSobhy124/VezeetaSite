using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;
using Vezeeta.ViewModel;

namespace Vezeeta.Controllers
{
    public class DoctorProfileController : Controller
    {
        private MyModel db = new MyModel();
        ViewModel1 mymodel = new ViewModel1();
        // GET: DoctorProfile
        public ActionResult DoctorProfile(int doctorID)
        {
            Session["DoctorID"] = doctorID;
            Doctor doctor = db.Doctors.FirstOrDefault(d => d.DoctorID == doctorID);
            ViewBag.city = db.Cities.FirstOrDefault(c => c.CityID == doctor.CityID).CityName;
            ViewBag.area = db.Areas.FirstOrDefault(c => c.AreaID == doctor.AreaID).AreaName;
            ViewBag.speciality = db.Specialties.FirstOrDefault(c => c.SpecialtyID == doctor.SpecialtyID).Specilty;
            Appointment appointment = new Appointment();
            mymodel.Appointment = appointment;
            mymodel.Doctor = doctor;
            
            //appointment.Date.ToString("D");
            mymodel.Appointment.Date = DateTime.Now.Date;
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult DoctorProfile(ViewModel1 mymodel)
        {
            bool IsRepeate=false;
            mymodel.Appointment.DoctorID = (int)Session["DoctorID"];

            Doctor doctor = db.Doctors.FirstOrDefault(d => d.DoctorID == mymodel.Appointment.DoctorID);
            ViewBag.city = db.Cities.FirstOrDefault(c => c.CityID == doctor.CityID).CityName;
            ViewBag.area = db.Areas.FirstOrDefault(c => c.AreaID == doctor.AreaID).AreaName;
            ViewBag.speciality = db.Specialties.FirstOrDefault(c => c.SpecialtyID == doctor.SpecialtyID).Specilty;
            if (User.Identity.IsAuthenticated)
            {
                var manager = new UserManager<MyIdentityUser>(new UserStore<MyIdentityUser>(new VezeetaIdentity()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                var email = currentUser.Email;
                Patient patient = db.Patients.FirstOrDefault(p => p.Email == email);
                Doctor doctor1 = db.Doctors.FirstOrDefault(a => a.Email == email);
                if (ModelState.IsValid)
                {
                    var appointmentuser = db.Appointments.FirstOrDefault(a => a.PatientID == patient.PatientID &&a.IsAttend==AppointmentStatus.Pending&&a.DoctorID==mymodel.Appointment.DoctorID ||a.PatientID==null);
                    if (appointmentuser!=null)
                    {
                        ModelState.AddModelError("", "يوجد لديك ميعاد يالفعل !!!!! ");

                    }
                    else { 
                    if (db.Appointments.Any(a => a.Time ==mymodel.Appointment.Time && a.Date==mymodel.Appointment.Date) && db.Appointments.Any(a => a.DoctorID == mymodel.Appointment.DoctorID))
                    {
                        ModelState.AddModelError("", "اختار ميعاد اخر هذا الميعاد محجوز مسبقا ");
                    }
                    else
                    {
                       
                            mymodel.Appointment.PatientID = patient.PatientID;
                            patient.DoctorID = mymodel.Appointment.DoctorID;
                            mymodel.Appointment.DoctorID = (int)Session["DoctorID"];
                            mymodel.Appointment.Status = AppointmentStatus.Pending;
                            mymodel.Appointment.IsAttend = AppointmentStatus.Pending;
                            db.Appointments.Add(mymodel.Appointment);
                            db.SaveChanges();
                            return RedirectToAction("PatientAppointments", "Appointment");
                    }
                    }
                }
                mymodel.Doctor = doctor;
                return View(mymodel);
            }
            return RedirectToAction("Login", "LogIn");
        }

        //[HttpPost]
        //public ActionResult DoctorProfile(ViewModel1 mymodel)
        //{
        //    mymodel.Appointment.DoctorID = (int)Session["DoctorID"];

        //    Doctor doctor = db.Doctors.FirstOrDefault(d => d.DoctorID == mymodel.Appointment.DoctorID);
        //    //ViewBag.city = new SelectList(db.Cities, "CityID", "CityName");
        //    //ViewBag.area = new SelectList(db.Areas, "AreaID", "AreaName");
        //    //ViewBag.speciality = new SelectList(db.Specialties, "SpecialtyID", "Specilty");
        //    ViewBag.city = db.Cities.FirstOrDefault(c => c.CityID == doctor.CityID).CityName;
        //    ViewBag.area = db.Areas.FirstOrDefault(c => c.AreaID == doctor.AreaID).AreaName;
        //    ViewBag.speciality = db.Specialties.FirstOrDefault(c => c.SpecialtyID == doctor.SpecialtyID).Specilty;
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var manager = new UserManager<MyIdentityUser>(new UserStore<MyIdentityUser>(new VezeetaIdentity()));
        //        var currentUser = manager.FindById(User.Identity.GetUserId());
        //        var email = currentUser.Email;
        //        Patient patient = db.Patients.FirstOrDefault(p => p.Email == email);

        //        if (ModelState.IsValid)
        //        {
        //            //Appointment aa = db.Appointments.FirstOrDefault();
        //            if (db.Appointments.Any(a=>a.Time.Equals(mymodel.Appointment.Time))|| db.Appointments.Any(a => a.DoctorID == mymodel.Appointment.DoctorID))
        //            {
        //                //if (db.Appointments.Any(a=>a.Ap`pointmentID==mymodel.Appointment.AppointmentID))
        //                //{

        //                //    return Content("انت حجزت عنده قبل كدا");

        //                //}
        //                //else
        //                //{
        //                    mymodel.Appointment.PatientID = patient.PatientID;
        //                    mymodel.Appointment.Status = AppointmentStatus.Pending;
        //                    mymodel.Appointment.IsRepeated = true;
        //                    mymodel.Doctor = doctor;
        //                    patient.DoctorID = mymodel.Appointment.DoctorID;
        //                //DateTime dateTime = new DateTime(2021, 6, 10, 12, 00, 00, 00);
        //                //mymodel.Appointment.Time = dateTime;
        //                //mymodel.Appointment.Date = DateTime.Now;

        //                db.Appointments.Add(mymodel.Appointment);
        //                db.SaveChanges();
        //                //return RedirectToAction("DoctorProfile", "DoctorProfile", new { doctorID = mymodel.Appointment.DoctorID });
        //                return View(mymodel);
        //                //}

        //            }
        //            else
        //            {

        //                mymodel.Appointment.PatientID = patient.PatientID;
        //                mymodel.Appointment.DoctorID = (int)Session["DoctorID"];
        //                mymodel.Appointment.Status = AppointmentStatus.Pending;
        //                mymodel.Appointment.IsRepeated = false;

        //                patient.DoctorID = mymodel.Appointment.DoctorID;
        //                db.Appointments.Add(mymodel.Appointment);
        //                db.SaveChanges();
        //                return RedirectToAction("PatientAppointments", "Appointment");
        //            }
        //        }
        //        mymodel.Doctor = doctor;
        //        return View(mymodel);
        //    }
        //    return RedirectToAction("Login", "LogIn");
        //}


    }
}