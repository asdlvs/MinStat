using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.Enterprises.WebUI.Models;

namespace MinStat.Enterprises.WebUI.Binders
{
    public class SummaryModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            const string activityString = "Activity";
            NameValueCollection form = controllerContext.HttpContext.Request.Form;
            SummaryModel model = new SummaryModel();

            model.Title = form["Title"];
            string[] activities = form.AllKeys.Where(x => x.StartsWith(activityString, StringComparison.OrdinalIgnoreCase)).ToArray();
            model.Activities = new int[activities.Count()];
            for (int i = 0; i < activities.Count(); i++)
            {
                model.Activities[i] = Int32.Parse(activities[i].Replace(activityString, ""));
            }

            return model;
        }
    }
}