using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vezeeta.Controllers
{
    public class RegisterDoctorController : Controller
    {
        // GET: RegisterDoctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterDoctor()
        {
            return View();

        }
    }
}