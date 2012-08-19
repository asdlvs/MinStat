using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinStat.AnalizeUI.Models;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public interface IInfoDataAdapter
    {
        IEnumerable<FederalDistrictModel> GetFederalDistricts();
        IEnumerable<FederalSubjectModel> GetFederalSubjects(int districtId);
        IEnumerable<EnterpriseModel> GetEnterprises(int subjectId);
        IEnumerable<ActivityModel> GetActivities();
        IEnumerable<FilterCriteryModel> GetFilterCriteries();
        IDictionary<int, string> GetPostLevels();
        IDictionary<int, string> GetEducationLevels();
    }
}