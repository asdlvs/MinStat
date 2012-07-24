using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MinStat.Enterprises.WebUI.Models;
using MinStat.Enterprises.WebUI.ServiceAdapters;

namespace MinStat.Enterprises.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationServiceAdapter _authenticationService;

        public AccountController(IAuthenticationServiceAdapter authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public AccountController() :this(new AuthenticationServiceAdapter())
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                bool isValid = _authenticationService.Login(model.UserName, model.Password, String.Empty,
                                                            model.RememberMe);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Неправильный логин или пароль");
            return View(model);
        }

    }
}
