using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public interface IInfoDataAdapter
    {
        IDictionary<int, string> GetFederalDistricts();
        IDictionary<int, string> GetFederalSubjects(int districtId);
        IDictionary<int, string> GetEnterprises(int subjectId);
    }
}