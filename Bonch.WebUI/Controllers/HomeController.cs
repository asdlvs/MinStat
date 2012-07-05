using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.WebUI.Models;

namespace Bonch.WebUI.Controllers
{
  using System.Security.Principal;

  using Bonch.Domain.POCO;
  using Bonch.Security;
  using Bonch.Security.Abstract;

  [Authorize]
  public class HomeController : Controller
  {
    //
    // GET: /Home/
    private IUserRepository _userRepository;
    public HomeController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public ActionResult Index()
    {
      return View();
    }


    [HttpGet]
    public JsonResult User()
    {
      //var user = new { Id = 1, Login = "asd@yandex.ru", FirstName = "Виталий", LastName = "Лебедев", Phone = "9217843487" };
      MinStatIdentity identity = (MinStatIdentity)this.HttpContext.User.Identity;
      return this.Json(identity, JsonRequestBehavior.AllowGet);
    }
    [HttpPost]
    public ActionResult UpdateUser(string firstName, string lastName, string phone)
    {
      MinStatIdentity identity = (MinStatIdentity)this.HttpContext.User.Identity;
      User user = _userRepository.GetUser(identity.Login);
      user.FirstName = HttpUtility.HtmlEncode(firstName);
      user.LastName = HttpUtility.HtmlEncode(lastName);
      user.Phone = HttpUtility.HtmlEncode(phone);
      _userRepository.SetUser(user);
      return null;
    }

    [HttpGet]
    public JsonResult HistoryPoints()
    {
      IEnumerable<HistoryPointViewModel> history = new List<HistoryPointViewModel> 
            {
                new HistoryPointViewModel{Id = "1", Date = DateTime.Now.AddDays(-5).ToShortDateString(), Author="Vitaliy Lebedev", ActionType="Action 1", Description="Добавил", CustomDescription="Мое описание"},
                new HistoryPointViewModel{Id = "2", Date = DateTime.Now.AddDays(-4).ToShortDateString(), Author="Vitaliy Lebedev", ActionType="Action 2", Description="Удалил", CustomDescription="Мое описание"},
                new HistoryPointViewModel{Id = "3", Date = DateTime.Now.AddDays(-3).ToShortDateString(), Author="Vitaliy Lebedev", ActionType="Action 1", Description="Добавил", CustomDescription="Мое описание"},
                new HistoryPointViewModel{Id = "4", Date = DateTime.Now.AddDays(-2).ToShortDateString(), Author="Vitaliy Lebedev", ActionType="Action 1", Description="Добавил", CustomDescription="Мое описание"},
                new HistoryPointViewModel{Id = "5", Date = DateTime.Now.AddDays(-1).ToShortDateString(), Author="Vitaliy Lebedev", ActionType="Action 3", Description="Изменил", CustomDescription="Мое описание"}
            };
      return this.Json(history, JsonRequestBehavior.AllowGet);
    }

  }
}
