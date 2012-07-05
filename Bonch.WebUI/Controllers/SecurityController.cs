using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Bonch.WebUI.Controllers
{
  using System.Web.Security;

  using Bonch.Domain.POCO;
  using Bonch.Security;
  using Bonch.Security.Abstract;
  using Bonch.WebUI.Models;

  public class SecurityController : Controller
  {
    //
    // GET: /Security/
    private IValidate _validator;

    private ISecurityHelper _helper;

    private IUserRepository _userRepository;

    public ActionResult LogOn(IValidate validator, ISecurityHelper helper, IUserRepository userRepository)
    {
      _validator = validator;
      _helper = helper;
      _userRepository = userRepository;
      return View();
    }

    [HttpPost]
    public ActionResult LogOn(LogOnModel model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        if (_validator.Validate(model.UserName, model.Password))
        {
          _helper.SetAuthCookie(_userRepository.GetUser(model.UserName), model.RememberMe);
          if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
              && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
          {
            return Redirect(returnUrl);
          }
          return this.RedirectToAction("Index", "Home");
        }
        this.ModelState.AddModelError("", "Неправильный логин или пароль.");
      }
      return View(model);
    }

    public ActionResult LogOff()
    {
      FormsAuthentication.SignOut();
      return RedirectToAction("Index", "Home");
    }

  }
}
