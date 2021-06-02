using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;
using Vezeeta.Services;
using Vezeeta.ViewModel;

namespace Vezeeta.Controllers
{
    public class RegisterController : Controller
    {
        MyModel db = new MyModel();

        private readonly UserManager<MyIdentityUser> userManager;
        //private readonly PatientService patientService; 
        public RegisterController()
        {
            var db = new VezeetaIdentity();
            var UserStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(UserStore);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        {
            MyIdentityUser user = this.userManager.FindById(Token);
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.EmailConfirmed = true;
                    await userManager.UpdateAsync(user);
                    //await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home", new { ConfirmedEmail = user.Email });
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Account", new { Email = "" });
            }
        }
        public ActionResult Register()
        {
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name");
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel userinfo )
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
                    creationResult = userManager.AddToRole(userId, "Patient");
                }
            }
            return View("RegisterPatient");

        }



        public ActionResult RegisterPatient()
        {
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name");
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterPatient([Bind(Include = "Name,Gender,PassWord,BirthDate,Address,Phone,Email")] Patient patient)
        {
            
           
            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name");
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName");
            //Patient p = db.Patients.FirstOrDefault(x => x.PatientID == patient.PatientID);
            //var identityUser = new MyIdentityUser();
            // identityUser.EmailConfirmed = false;
            //var email = identityUser.Email;
            //var pass =p.PassWord;
            if (ModelState.IsValid)
            {

                    db.Patients.Add(patient);
                    db.SaveChanges();

                return RedirectToAction("HomePage", "doctors");
            }

            ViewBag.AdminID = new SelectList(db.Admins, "AdminID", "Name", patient.AdminID);
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "FName", patient.DoctorID);
            return RedirectToAction("HomePage", "doctors");

        }
        [AllowAnonymous]
        public ActionResult Confirm(string Email)
        {
            ViewBag.Email = Email; return View();
        }

    }
}