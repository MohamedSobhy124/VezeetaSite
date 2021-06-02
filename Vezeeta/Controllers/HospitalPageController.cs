using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;

namespace Vezeeta.Controllers
{
    public class HospitalPageController : Controller
    {
        // GET: HospitalPage
        private MyModel db = new MyModel();
        
        public ActionResult HospitalPage()
        {
            return View(db.Doctors.ToList());

        }
    }
}