using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;


[assembly: OwinStartup(typeof(Vezeeta.App_Start.Startup))]

namespace Vezeeta.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            CookieAuthenticationOptions cookieAuthOptions = new CookieAuthenticationOptions();
            cookieAuthOptions.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            cookieAuthOptions.LoginPath = new PathString("/LogIn/Login");
           

            app.UseCookieAuthentication(cookieAuthOptions);
        }
    }
}
