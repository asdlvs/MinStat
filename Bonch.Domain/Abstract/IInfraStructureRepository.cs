using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;
namespace Bonch.Domain.Abstract
{
    public interface IInfraStructureRepository
    {
        IEnumerable<FederalDistrict> GetFederalDistricts();

        IEnumerable<FederalSubject> GetFederationSubjects(int federalDictrictId);

        IEnumerable<Enterprise> GetEnterprises(int federalSubjectId);

        Enterprise AddEnterprise(Enterprise enterprise);

        void DeleteEnterprise(Enterprise enterprise);

        FederalSubject AddFederalSubject(FederalSubject federationSubject);

        FederalDistrict AddFederalDistrict(FederalDistrict federalDistrict);
    }
}
