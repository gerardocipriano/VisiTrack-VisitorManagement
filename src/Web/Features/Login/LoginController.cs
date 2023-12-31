using Web.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Core.Services.Shared;
using System.Threading.Tasks;
using Core.Infrastructure;

namespace Web.Features.Login
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Alerts]
    [ModelStateToTempData]
    public partial class LoginController : Controller
    {
        public static string LoginErrorModelStateKey = "LoginError";
        private readonly SharedService _sharedService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public LoginController(SharedService sharedService, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedService = sharedService;
            _sharedLocalizer = sharedLocalizer;
        }

        private ActionResult LoginAndRedirect(UserDetailDTO utente, string returnUrl, bool rememberMe)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, utente.Id.ToString()),
        new Claim(ClaimTypes.Email, utente.Email)
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
            {
                ExpiresUtc = (rememberMe) ? DateTimeOffset.UtcNow.AddMonths(3) : null,
                IsPersistent = rememberMe,
            });

            // List of authorized redirects
            var authorizedRedirects = new List<string> { "/NewVisitor", "/Reports", "/Visitorslist" };

            if (!string.IsNullOrWhiteSpace(returnUrl) && authorizedRedirects.Contains(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction(MVC.NewVisitor.Index());
        }

        [HttpGet]
        public virtual IActionResult Login(string returnUrl)
        {
            // List of authorized redirects
            var authorizedRedirects = new List<string> { "/NewVisitor", "/Reports", "/Visitorslist" };

            if (HttpContext.User != null && HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl) && authorizedRedirects.Contains(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction(MVC.NewVisitor.Index());
            }

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async virtual Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var utente = await _sharedService.Query(new CheckLoginCredentialsQuery
                    {
                        Email = model.Email,
                        Password = model.Password,
                    });

                    return LoginAndRedirect(utente, model.ReturnUrl, model.RememberMe);
                }
                catch (LoginException e)
                {
                    ModelState.AddModelError(LoginErrorModelStateKey, e.Message);
                }
            }

            return RedirectToAction(MVC.Login.Login());
        }

        [HttpPost]
        public virtual IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            Alerts.AddSuccess(this, "Utente scollegato correttamente");
            return RedirectToAction(MVC.Login.Login());
        }
    }
}