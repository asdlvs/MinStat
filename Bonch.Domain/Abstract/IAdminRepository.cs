using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;
namespace Bonch.Domain.Abstract
{
    public interface IAdminRepository
    {
        IEnumerable<FederalDistrict> GetFederalDistricts();

        IEnumerable<FederationSubject> GetFederationSubjects(FederalDistrict federalDistrict);

        IEnumerable<Enterprise> GetEnterprises(FederationSubject federationSubject);

        Enterprise AddEnterprise(Enterprise enterprise);

        void DeleteEnterprise(Enterprise enterprise);

        FederationSubject AddFederalSubject(FederationSubject federationSubject);

        FederalDistrict AddFederalDistrict(FederalDistrict federalDistrict);
    }
}
