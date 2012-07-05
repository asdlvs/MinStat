using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonch.WebUI.Models;
using System.Security.Principal;
using Bonch.Domain.POCO;
using Bonch.Security;
using Bonch.Security.Abstract;
using Bonch.Security.Concrete;

namespace Bonch.WebUI.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private IUserRepository _userRepository;
        private ISecurityHelper _helper;
        public HomeController(IUserRepository userRepository, ISecurityHelper helper)
        {
            _userRepository = userRepository;
            _helper = helper;
        }

        public ActionResult Index()
        {
            MinStatIdentity identity = (MinStatIdentity)this.HttpContext.User.Identity;
            ViewBag.IsDataFilled = identity.IsDataFilled();
            UserViewModel model = new UserViewModel
            {
                Id = identity.Id.ToString(),
                FirstName = identity.FirstName,
                LastName = identity.LastName,
                Phone = identity.Phone,
                Login = identity.Login
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                MinStatIdentity identity = (MinStatIdentity)this.HttpContext.User.Identity;
                User user = _userRepository.GetUser(identity.Mail);
                user.FirstName = HttpUtility.HtmlEncode(model.FirstName);
                user.LastName = HttpUtility.HtmlEncode(model.LastName);
                user.Phone = HttpUtility.HtmlEncode(model.Phone);
                _userRepository.SetUser(user);
                _helper.SetAuthCookie(user, true);
                ViewBag.IsDataFilled = identity.IsDataFilled();
                return RedirectToAction("Index");
            }
            this.ModelState.AddModelError("", "Необходимо указать данные для всех полей!");
            return View(model);
        }

        public PartialViewResult HistoryPoints()
        {
            MinStatIdentity identity = (MinStatIdentity)this.HttpContext.User.Identity;
            if (identity.IsDataFilled())
            {
                IEnumerable<HistoryPointViewModel> history = new List<HistoryPointViewModel> 
                {
                    new HistoryPointViewModel{Id = "1", Date = DateTime.Now.AddDays(-5).ToShortDateString(), Author="Vitaliy Lebedev", ActionType="Action 1", Description="Добавил", CustomDescription="Мое описание"},
                    new HistoryPointViewModel{Id = "2", Date = DateTime.Now.AddDays(-4).ToShortDateString(), Author="Vitaliy Lebedev", ActionType="Action 2", Description="Удалил", CustomDescription="Мое описание"},
                    new HistoryPointViewModel{Id = "3", Date = DateTime.Now.AddDays(-3).ToShortDateString(), Author="Vitaliy Lebedev", ActionType="Action 1", Description="Добавил", CustomDescription="Мое описание"},
                    new HistoryPointViewModel{Id = "4", Date = DateTime.Now.AddDays(-2).ToShortDateString(), Author="Vitaliy Lebedev", ActionType="Action 1", Description="Добавил", CustomDescription="Мое описание"},
                    new HistoryPointViewModel{Id = "5", Date = DateTime.Now.AddDays(-1).ToShortDateString(), Author="Vitaliy Lebedev", ActionType="Action 3", Description="Изменил", CustomDescription="Мое описание"}
                };
                return PartialView(history);
            }
            return null;
        }

    }
}
