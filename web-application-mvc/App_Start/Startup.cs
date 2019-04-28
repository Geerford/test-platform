using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using static web_application_mvc.App_Start.Authentication;

[assembly: OwinStartup(typeof(web_application_mvc.App_Start.Startup))]

namespace web_application_mvc.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = PrismAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "PrismSystem", 
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromHours(6)
            });
        }
    }
}