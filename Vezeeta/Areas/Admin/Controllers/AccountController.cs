using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Areas.Admin.Models;
using Vezeeta.Services;

namespace Vezeeta.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginInfo)
        {
            var adminService = new AdminServices();
            var isLogedIn = adminService.Login(loginInfo.Email, loginInfo.PassWord);
            if (isLogedIn)
            {
                return RedirectToAction("index", "Default");
            }
            else
            {
                loginInfo.Massage = "Email Or PassWord Is Incorrect...";
                return View(loginInfo);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            var owincontext = Request.GetOwinContext();
            var authManager = owincontext.Authentication;
            authManager.SignOut("ApplicationCookie");
            Session.Abandon();
            return RedirectToAction("HomePage", "Doctors");
        }
    }
}