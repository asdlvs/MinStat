using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bonch.WebUI.Models;
using Bonch.WebUI.Infrustructure;
using Bonch.Security.Concrete;

namespace Bonch.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            SecurityHelper sh = new SecurityHelper();
            Context.User = sh.GetAuthCookie();
        } 


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);


            ModelBinders.Binders.Add(typeof(SummaryViewModel), new JavaScriptModelBinder<SummaryViewModel>("summary"));
            ModelBinders.Binders.Add(typeof(ActivityViewModel), new JavaScriptModelBinder<ActivityViewModel>("activity"));
            ModelBinders.Binders.Add(typeof(PersonViewModel), new JavaScriptModelBinder<PersonViewModel>("person"));
            ModelBinders.Binders.Add(typeof(IEnumerable<ActivityViewModel>), new EnumOfActivityViewModelBinder());

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            

        }
    }
}