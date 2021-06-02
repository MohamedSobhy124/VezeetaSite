using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;
using Vezeeta.MyFilters;

namespace Vezeeta.Controllers
{
   //[MyAuthorize]
    public class HomePageController : Controller
    {
        private MyModel  db = new MyModel();

        // GET: HomePage
        public ActionResult HomePage()
        {
            return View(db.Doctors.ToList());
        }
    }
}