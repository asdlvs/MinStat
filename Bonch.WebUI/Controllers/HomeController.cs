using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.WebUI.Models;

namespace Bonch.WebUI.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    //
    // GET: /Home/

    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public JsonResult User()
    {
      var user = new { Id = 1, Login = "asd@yandex.ru", FirstName = "Виталий", LastName = "Лебедев", Phone = "9217843487" };
      return this.Json(user, JsonRequestBehavior.AllowGet);
    }
    [HttpPost]
    public ActionResult UpdateUser(string firstName, string lastName, string phone)
    {
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
