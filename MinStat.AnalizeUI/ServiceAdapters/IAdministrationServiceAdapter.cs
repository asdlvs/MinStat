using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public  interface IAdministrationServiceAdapter
    {
        void CreateEnterpise(string enterpriseTitle, int federalSubjectId, string mail);

        void RemoveEnterprise(int enterpriseId);
    }
}
