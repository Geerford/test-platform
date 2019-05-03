using Application.Interfaces;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_application_mvc.App_Start;
using web_application_mvc.Models;
using static web_application_mvc.App_Start.Authentication;

namespace web_application_mvc.Controllers
{
    public class AccountController : Controller
    {
        IUserService service;
        IRoleService roleService;
        IActivityService activityService;

        public AccountController(IUserService service, IRoleService roleService, IActivityService activityService)
        {
            this.service = service;
            this.roleService = roleService;
            this.activityService = activityService;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public virtual ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IAuthenticationManager authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var authService = new Authentication(authenticationManager, service);
                var authenticationResult = authService.SignIn(model);
                if (authenticationResult.IsSuccess)
                {
                    Core.Activity firstActivity = activityService.GetAll().FirstOrDefault();
                    Core.Activity lastActivity = activityService.GetAll().LastOrDefault();
                    if(lastActivity.Date.Day != DateTime.Now.Day)
                    {
                        activityService.Create(new Core.Activity
                        {
                            Date = DateTime.Now,
                            UserID = service.GetAll().FirstOrDefault(x => x.Email.Equals(model.Email)).ID
                        });
                    }                    
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError("", authenticationResult.ErrorMessage);
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Logout()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(PrismAuthentication.ApplicationCookie);

            return RedirectToAction("Login", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if (service.GetAll().FirstOrDefault(x => x.Email.Equals(model.Email)) != null)
            {
                ModelState.AddModelError("Email", "Данная электронная почта уже зарегистрирована");
            }
            if (ModelState.IsValid)
            {
                service.Create(new Core.User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Midname = model.Midname,
                    Email = model.Email,
                    Password = model.Password,
                    Phone = model.Phone,
                    RoleID = roleService.GetAll().FirstOrDefault(x => x.Value.Equals("Студент")).ID
                });
                IAuthenticationManager authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var authService = new Authentication(authenticationManager, service);
                var authenticationResult = authService.SignIn(new LoginViewModel
                {
                    Email = model.Email,
                    Password = model.Password
                });
                return RedirectToLocal(returnUrl);
            }
            return View(model);
        }
    }
}