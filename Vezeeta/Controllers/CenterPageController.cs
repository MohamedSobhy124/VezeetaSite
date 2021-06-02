using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;

namespace Vezeeta.Controllers
{
    public class CenterPageController : Controller
    {
        // GET: CenterPage
        private MyModel db = new MyModel();

        public ActionResult Center()
        {
            return View(db.Doctors.ToList());

        }
    }
}