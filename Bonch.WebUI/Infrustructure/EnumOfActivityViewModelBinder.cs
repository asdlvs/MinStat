using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Bonch.WebUI.Models;

namespace Bonch.WebUI.Infrustructure
{
    public class EnumOfActivityViewModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string activityKey = "activities";
            HttpRequestBase request = controllerContext.HttpContext.Request;
            if (request.Form.AllKeys.Contains(activityKey))
            {
                string jsonActivities = request.Form[activityKey];
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Deserialize<IEnumerable<ActivityViewModel>>(jsonActivities);
            }

            return null;
        }
    }
}