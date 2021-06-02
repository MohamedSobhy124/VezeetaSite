using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vezeeta.Models;

namespace Vezeeta.Controllers
{
    public class InsurancePageController : Controller
    {
        private MyModel db = new MyModel();
        // GET: InsurancePage

        public ActionResult InsurancePage()
        {
            return View(db.Insurances.ToList());

        }
        public ActionResult ins(string insurance = null, string sps = null)
        {

            if (insurance != null)
            {
                List<Insurance> insurances = db.Insurances.Where(d => d.CompanyName.ToLower().Contains(insurance)).ToList();
                return View("Index", insurances);
            }
            else
                return View("Index");
        }
    }
}