using Application.Interfaces;
using Core;
using Microsoft.Owin.Security;
using System.Linq;
using System.Security.Claims;
using web_application_mvc.Models;

namespace web_application_mvc.App_Start
{
    public class Authentication
    {
        private readonly IAuthenticationManager authenticationManager;
        IUserService service;

        public Authentication(IAuthenticationManager authenticationManager, IUserService service)
        {
            this.authenticationManager = authenticationManager;
            this.service = service;
        }
        
        public static class PrismAuthentication
        {
            public const string ApplicationCookie = "AuthenticationType";
        }

        public class AuthenticationResult
        {
            public string ErrorMessage { get; private set; }
            public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);

            public AuthenticationResult(string ErrorMessage = null)
            {
                this.ErrorMessage = ErrorMessage;
            }
        }

        public AuthenticationResult SignIn(LoginViewModel model)
        {
            User user = service.GetAll().FirstOrDefault(u => u.Email.Equals(model.Email) 
                && u.Password.Equals(model.Password));
            if (user == null)
            {
                return new AuthenticationResult("Неверно введен логин или пароль.");
            }
            else
            {
                var claim = CreateClaim(user);
                authenticationManager.SignOut(PrismAuthentication.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, claim);
                return new AuthenticationResult();
            }
        }

        private ClaimsIdentity CreateClaim(User user)
        {
            var claim = new ClaimsIdentity(PrismAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider"));
            claim.AddClaim(new Claim(ClaimTypes.Name, user.Name + " " + user.Surname));
            claim.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Value));
            return claim;
        }
    }
}