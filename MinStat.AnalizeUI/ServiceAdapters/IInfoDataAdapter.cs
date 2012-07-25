using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinStat.AnalizeUI.Models;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public interface IInfoDataAdapter
    {
        IDictionary<int, string> GetFederalDistricts();
        IDictionary<int, string> GetFederalSubjects(int districtId);
        IDictionary<int, string> GetEnterprises(int subjectId);
        IEnumerable<ActivityModel> GetActivities();
        IEnumerable<FilterCriteryModel> GetFilterCriteries();
    }
}