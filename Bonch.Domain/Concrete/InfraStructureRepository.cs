using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.Abstract;

namespace Bonch.Domain.Concrete
{
    public class InfraStructureRepository : IInfraStructureRepository
    {

        MinStatDbContext _context;
        public InfraStructureRepository()
        {
            _context = new MinStatDbContext();
        }
        public IEnumerable<POCO.FederalDistrict> GetFederalDistricts()
        {
            return _context.FederalDistricts;
        }

        public POCO.Enterprise AddEnterprise(POCO.Enterprise enterprise)
        {
            throw new NotImplementedException();
        }

        public void DeleteEnterprise(POCO.Enterprise enterprise)
        {
            throw new NotImplementedException();
        }

        public POCO.FederalSubject AddFederalSubject(POCO.FederalSubject federationSubject)
        {
            throw new NotImplementedException();
        }

        public POCO.FederalDistrict AddFederalDistrict(POCO.FederalDistrict federalDistrict)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<POCO.FederalSubject> GetFederationSubjects(int federalDictrictId)
        {
            return _context.FederalSubjects.Where(x => x.FederalDistrictId == federalDictrictId);
        }

        public IEnumerable<POCO.Enterprise> GetEnterprises(int federalSubjectId)
        {
            return _context.Enterprises.Where(x => x.FederalSubjectId == federalSubjectId);
        }
    }
}
