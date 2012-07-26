using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Models;

namespace MinStat.AnalizeUI.Binders
{
    public class ConsolidateReportCreatorBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ConsolidateReportCreatorModel model = new ConsolidateReportCreatorModel();
            const string verticalMark = "filtercritery_";
            const string horizontalMark = "activitycheckbox_";
            NameValueCollection form = controllerContext.HttpContext.Request.Form;
            var activitiesCollection = new List<int>();
            var filterCriteryCollection = new List<int>();
            foreach (var formItem in form.AllKeys)
            {
                if (form[formItem].IndexOf(Boolean.TrueString, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (formItem.StartsWith(horizontalMark))
                    {
                        int item = Int32.Parse(formItem.Replace(horizontalMark, ""));
                        activitiesCollection.Add(item);
                    }
                    else if (formItem.StartsWith(verticalMark))
                    {
                        int item = Int32.Parse(formItem.Replace(verticalMark, ""));
                        filterCriteryCollection.Add(item);
                    }
                }
            }
            model.SelectedActivities = activitiesCollection;
            model.SelectedCriteries = filterCriteryCollection;

            int federalDistrictId = 0;
            if (Int32.TryParse(form["federalDistrictId"], out federalDistrictId))
            {
                model.FederalDistrictId = federalDistrictId;
            }
            int federalSubjectId = 0;

            if (Int32.TryParse(form["federalSubjectId"], out federalSubjectId))
            {
                model.FederalSubjectId = federalSubjectId; 
            }

            int enterpriseId = 0;

            if (Int32.TryParse(form["enterpriseId"], out enterpriseId))
            {
                model.EnterpriseId = enterpriseId; 
            }

            model.StartDate = form["startDate"];
            model.EndDate = form["endDate"];
            return model;
        }
    }
}