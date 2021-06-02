using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vezeeta.MyFilters
{
    public class MyAuthorize: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string email = filterContext.HttpContext.Session["Email"]?.ToString();
            if (email == null)
                filterContext.Result = new RedirectResult("/LogIn/Login");

        }
    }
}