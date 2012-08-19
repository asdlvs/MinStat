using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.DAL;
using MinStat.Enterprises.DAL;

namespace MinStat.Enterprise.FillHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            EnterpriseDataRepository enterpriseDataRepository = new EnterpriseDataRepository();
            StatisticDataRepository statisticDataRepository = new StatisticDataRepository();
            var enterprises = statisticDataRepository.GetEnterprises(0);

            var summaries =
                enterprises.Select(
                    x => new {EnterpriseId = x.Id, Summaries = enterpriseDataRepository.GetSummaries(x.Id)});

            foreach (var summary in summaries)
            {
                foreach (var i in summary.Summaries)
                {
                    enterpriseDataRepository.CopySummary(summary.EnterpriseId, "2 квартал 2011", i.Id);
                    Console.WriteLine(String.Format("{0} - {1}", summary.EnterpriseId, i.Id));
                }
            }
        }
    }
}
