using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using MinStat.AnalizeUI.O;

namespace MinStat.AnalizeUI.Binders
{
    public class CustomReportBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            const string verticalMark = "activitycheckbox_";
            const string horizontalMark = "horizontal_";
            NameValueCollection form = controllerContext.HttpContext.Request.Form;
            var horizontalChecks = new List<KeyValuePair<int, int>>();
            var verticalChecks = new List<int>();
            foreach (var formItem in form.AllKeys)
            {
                if (form[formItem].IndexOf(Boolean.TrueString, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (formItem.StartsWith(horizontalMark))
                    {
                        int[] coords = formItem.Replace(horizontalMark, "").Split('-').Select(Int32.Parse).ToArray();
                        horizontalChecks.Add(new KeyValuePair<int, int>(coords[0], coords[1]));
                    }
                    else if (formItem.StartsWith(verticalMark))
                    {
                        int coords = Int32.Parse(formItem.Replace(verticalMark, ""));
                        verticalChecks.Add(coords);
                    }
                }
            }

            return new SelectionChecks {HorizontalChecks = horizontalChecks, VerticalChecks = verticalChecks};

        }
    }
}