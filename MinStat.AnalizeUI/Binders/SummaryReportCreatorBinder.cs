using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinStat.AnalizeUI.Models;

namespace MinStat.AnalizeUI.Binders
{
    public class SummaryReportCreatorBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            SummaryReportCreatorModel model = new SummaryReportCreatorModel();
            const string activitiesMark = "activitycheckbox_";
            const string gendersMark = "gendercheckbox_";
            const string educationLevelsMark = "educationlevelcheckbox_";
            const string postLevelsMark = "postlevelcheckbox_";

            NameValueCollection form = controllerContext.HttpContext.Request.Form;
            var activitiesCollection = new List<int>();
            var gendersCollection = new List<int>();
            var educationLevelsCollection = new List<int>();
            var postLevelsCollection = new List<int>();

            foreach (var formItem in form.AllKeys)
            {
                if (form[formItem].IndexOf(Boolean.TrueString, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (formItem.StartsWith(activitiesMark))
                    {
                        int item = Int32.Parse(formItem.Replace(activitiesMark, ""));
                        activitiesCollection.Add(item);
                    }
                    else if (formItem.StartsWith(gendersMark))
                    {
                        int item = Int32.Parse(formItem.Replace(gendersMark, ""));
                        gendersCollection.Add(item);
                    }
                    else if (formItem.StartsWith(educationLevelsMark))
                    {
                        int item = Int32.Parse(formItem.Replace(educationLevelsMark, ""));
                        educationLevelsCollection.Add(item);
                    }
                    else if (formItem.StartsWith(postLevelsMark))
                    {
                        int item = Int32.Parse(formItem.Replace(postLevelsMark, ""));
                        postLevelsCollection.Add(item);
                    }
                }
            }
            model.SelectedActivities = activitiesCollection;
            model.SelectedEducationLevels = educationLevelsCollection;
            model.SelectedPostLevels = postLevelsCollection;
            model.SelectedGenders = gendersCollection;

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

            model.BoundDate = form["endDate"];
            return model;
        }
    }
}