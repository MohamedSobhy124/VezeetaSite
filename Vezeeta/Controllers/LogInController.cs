namespace Vezeeta.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Vezeeta.Models;
    using Vezeeta.ViewModel;
    public class LogInController : Controller
    {
        MyModel db = new MyModel();
        private readonly UserManager<MyIdentityUser> userManager;
        public LogInController()
        {
            var db = new VezeetaIdentity();
            var UserStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(UserStore);
        }
        public ActionResult Login(string ReturnUrl = "")
        {
            return View(new SignInViewModel
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(SignInViewModel LoginData)
        {
            if (ModelState.IsValid)
            {
                if(LoginData.Password == null)
                {
                    return View(LoginData);
                }
                var existUser = await userManager.FindAsync(LoginData.Email, LoginData.Password);
                if (existUser != null)
                {
                    await SignIn(existUser);

                    if (!string.IsNullOrEmpty(LoginData.ReturnUrl))
                    {
                        return Redirect(LoginData.ReturnUrl);
                    }
                    var userRoles = userManager.GetRoles(existUser.Id);
                    var role = userRoles.FirstOrDefault();
                    if (role == "Admin")
                    {
                        return RedirectToAction("index", "Default", new { Area = "Admin" });
                    }
                    return RedirectToAction("HomePage", "Doctors");
                }
            }
            LoginData.Message = "Email Or PassWord Is Incorrect !!";
            return View(LoginData);
        }
        private async Task SignIn(MyIdentityUser myIdentityUser)
        {
            var identity = await userManager.CreateIdentityAsync(myIdentityUser, DefaultAuthenticationTypes.ApplicationCookie);
            var owincontext = Request.GetOwinContext();
            var authManager = owincontext.Authentication;
            authManager.SignIn(identity);
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
