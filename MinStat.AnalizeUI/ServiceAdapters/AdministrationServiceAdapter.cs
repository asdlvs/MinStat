using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MinStat.AnalizeUI.AdministrationService;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public class AdministrationServiceAdapter : IAdministrationServiceAdapter
    {
        public void CreateEnterpise(string enterpriseTitle, int federalSubjectId, string mail)
        {
            using (AdministrationServiceClient proxy = new AdministrationServiceClient())
            {
                proxy.CreateEnterprise(enterpriseTitle, federalSubjectId, mail);
            }
        }


        public void RemoveEnterprise(int enterpriseId)
        {
            using (AdministrationServiceClient proxy = new AdministrationServiceClient())
            {
                proxy.RemoveEnterprise(enterpriseId);
            }
        }
    }
}