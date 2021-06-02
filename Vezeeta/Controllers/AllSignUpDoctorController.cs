using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;
using Vezeeta.ViewModel;

namespace Vezeeta.Controllers
{
    public class AllSignUpDoctorController : Controller
    {
        MyModel db = new MyModel();
        private readonly UserManager<MyIdentityUser> userManager;
        //private readonly PatientService patientService; 
        public AllSignUpDoctorController()
        {
            var db = new VezeetaIdentity();
            var UserStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(UserStore);
        }

        public ActionResult RegisterDoctor()
        {
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name");
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterDoctor(RegisterViewModel userinfo)
        {
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name");
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");

            if (ModelState.IsValid)
            {
                var identityUser = new MyIdentityUser
                {
                    Email = userinfo.Email,
                    UserName = userinfo.Email,
                };
                if (userinfo.Password == null)
                {
                    return View(userinfo);

                }
                var creationResult = await userManager.CreateAsync(identityUser, userinfo.Password);
                //user Created
                if (creationResult.Succeeded)
                {
                    var userId = identityUser.Id;
                    creationResult = userManager.AddToRole(userId,"Doctor");
                }
            }
            return View("SignUpDoctor");
        }


        public ActionResult SignUpDoctor()
        {

            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name");
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName");
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty");
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName");


            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUpDoctor([Bind(Include = "IDImage,DoctorStatus,InsuranceID,FName,LName,PassWord,branchName,BirthDate,ExamineFee,Image,Gender,Title,WaitingTime,SpecialtyID,Phone,AreaID,AddressDetails,CityID,Email,Entity,EntityName,AdminID")] Doctor doctor, IEnumerable<HttpPostedFileBase> DocImg)
        {
            if (ModelState.IsValid)
            {
                if (db.Doctors.Any(p => p.Email == doctor.Email))
                {
                    ModelState.AddModelError("", "Doctor already exists.");
                }
                else
                {
                    doctor.DoctorStatus = AppointmentStatus.Pending;
                    db.Doctors.Add(doctor);
                    db.SaveChanges();

                    //string uppload = "/images/noimage.jpg";
                    //string uppload = "";
                    string img = "";

                    foreach (var item in DocImg)
                    {
                        if (item != null)
                        {
                            string filename = Guid.NewGuid() + Path.GetExtension(item.FileName);
                            string filepath = "/images/" + filename;
                            item.SaveAs(Path.Combine(Server.MapPath("/images/"), filename));
                            //uppload += filepath + ":";
                            img += filepath + ":";
                        }
                    }
                    string[] patharray = img.Split(':');
                    //string[] patharray = uppload.Split(':');
                    doctor.Image = patharray[0].ToString();
                    doctor.IDImage = patharray[1].ToString();
                    db.SaveChanges();
                    return RedirectToAction("homepage", "doctors");
                }
            }

            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", doctor.AdminID);
            ViewBag.CityID = new SelectList(db.Cities, "CityID", "CityName", doctor.CityID);
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Specilty", doctor.SpecialtyID);
            ViewBag.InsuranceID = new SelectList(db.Insurances, "InsuranceID", "CompanyName", doctor.InsuranceID);

            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "AreaName");
            return View(doctor);
        }
        public JsonResult GetStateById(int CityID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Areas.Where(a => a.CityID == CityID),JsonRequestBehavior.AllowGet );
        }

       
    }
}