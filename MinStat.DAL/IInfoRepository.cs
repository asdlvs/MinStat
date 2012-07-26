using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO;

namespace MinStat.DAL
{
    public interface IInfoRepository
    {
        void CreateEnterprise(string enterpriseTitle, int federalSubjectId, string mail);

        void RemoveEnterprise(int enterpriseId);
    }
}
