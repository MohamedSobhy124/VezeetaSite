namespace Vezeeta.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Vezeeta.Models;
    using Vezeeta.ViewModel;

    /// <summary>
    /// Defines the <see cref="Account2Controller" />.
    /// </summary>
    public class Account2Controller : Controller
    {
        /// <summary>
        /// Defines the userManager.
        /// </summary>
        private readonly UserManager<MyIdentityUser> userManager;
       
        public Account2Controller()
        {
            var db = new VezeetaIdentity();
            var UserStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(UserStore);
        }

        /// <summary>
        /// The Register.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// The Register.
        /// </summary>
        /// <param name="userinfo">The userinfo<see cref="RegisterViewModel"/>.</param>
        /// <returns>The <see cref="Task{ ActionResult}"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel userinfo)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new MyIdentityUser
                {
                    Email = userinfo.Email,
                    UserName = userinfo.Email,
                };
                

                var creationResult = await userManager.CreateAsync(identityUser, userinfo.Password);
                

                if (creationResult.Succeeded)
                {
                    var userId = identityUser.Id;
                    userManager.AddToRole(userId,"Admin");
                    return RedirectToAction("Index", "Default", new { Area = "Admin" });
                }
           

            }
            

            return View();
        }

        // GET: Account
        /// <summary>
        /// The Login.
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult}"/>.</returns>
        public async Task<ActionResult> Login()
        {
            //var user = await userManager.CreateAsync(new MyIdentityUser
            //{
            //    Email = "MO@M.COM",
            //    UserName = "Mhamed"
            //}, "123456");
            //ViewBag.User = user.Succeeded;

            return View();
        }
    }
}
