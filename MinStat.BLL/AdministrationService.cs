using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.DAL;

namespace MinStat.BLL
{
    public class AdministrationService : IAdminstrationService
    {
        private IInfoRepository _infoRepository;

        public AdministrationService(IInfoRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }

        public AdministrationService()
            : this(new InfoRepository())
        {
        }

        public void CreateEnterprise(string enterpriseTitle, int federalSubjectId ,string mail)
        {
            _infoRepository.CreateEnterprise(enterpriseTitle, federalSubjectId, mail);
        }


        public void RemoveEnterprise(int enterpriseId)
        {
            _infoRepository.RemoveEnterprise(enterpriseId);
        }
    }
}
