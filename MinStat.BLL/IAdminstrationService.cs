using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MinStat.BLL
{
    [ServiceContract(Name = "AdministrationService")]
    public interface IAdminstrationService
    {
        [OperationContract]
        void CreateEnterprise(string enterpriseTitle, int federalSubjectId, string mail);

        [OperationContract]
        void RemoveEnterprise(int enterpriseId);
    }
}
