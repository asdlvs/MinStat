using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MinStat.DAL.POCO.ReportItems;
using MinStat.DAL.POCO.ResultItems;

namespace MinStat.DAL.Converters
{
    class ConsolidatedReportConverter : IStatisticDataConverter<ConsolidatedReportItem>
    {
        //TODO: Неправильно считается
        public IEnumerable<StatisticData> Convert(IEnumerable<ConsolidatedReportItem> result)
        {
            int summariesCount = result.GroupBy(x => x.SummaryId).Count();
            int avgPeopleCount = result.Count() / summariesCount;

            #region WorkersData
            int mensCount = result.Count(x => x.Gender) / summariesCount;
            int womensCount = avgPeopleCount - mensCount;
            int hiringCount = result.Count(x => x.IsHiring) / summariesCount; 
            int dismissedCount = result.Count(x => x.IsDissmissed) / summariesCount; 
            int increaseQulificationCount = result.Count(x => x.WasQualificationIncrease) / summariesCount; 
            int attestateCount = result.Count(x => x.WasValidate) / summariesCount; 

            StatisticData statisticWorkersData = new StatisticData
                                              {
                                                  Titles = new Dictionary<string, string>()
                                                               {
                                                                   {"1","Число работников, всего, чел."},
                                                                   {"2",avgPeopleCount.ToString(CultureInfo.InvariantCulture)},
                                                                   {"3","100%"}
                                                               }
                                              };
            statisticWorkersData.Lines = new List<StatisticDataItem>
                                      {
                                          new StatisticDataItem{StrongLevel = 0, Title = "из них мужчин", Values = new List<string>{mensCount.ToString(CultureInfo.InvariantCulture), GetPersentage(mensCount, avgPeopleCount)}},
                                          new StatisticDataItem{StrongLevel = 0, Title = "из них женщин", Values = new List<string>{womensCount.ToString(CultureInfo.InvariantCulture), GetPersentage(womensCount, avgPeopleCount)}},
                                          new StatisticDataItem{StrongLevel = 0, Title = "Принято на работу, чел.", Values = new List<string>{hiringCount.ToString(CultureInfo.InvariantCulture), GetPersentage(hiringCount, avgPeopleCount)}},
                                          new StatisticDataItem{StrongLevel = 0, Title = "Уволено с работы, чел.", Values = new List<string>{dismissedCount.ToString(CultureInfo.InvariantCulture), GetPersentage(dismissedCount, avgPeopleCount)}},
                                          new StatisticDataItem{StrongLevel = 0, Title = "Повысили квалификацию, чел.", Values = new List<string>{increaseQulificationCount.ToString(CultureInfo.InvariantCulture), GetPersentage(increaseQulificationCount, avgPeopleCount)}},
                                          new StatisticDataItem{StrongLevel = 0, Title = "Прошли аттестацию, чел.", Values = new List<string>{attestateCount.ToString(CultureInfo.InvariantCulture), GetPersentage(attestateCount, avgPeopleCount)}},
                                      };
            #endregion

            #region Education Data

            int educationCount = avgPeopleCount;
            int vpoCount = result.Count(x => x.EducationLevelId == 3) / summariesCount;
            int spoCount = result.Count(x => x.EducationLevelId == 2) / summariesCount;
            int soCount = result.Count(x => x.EducationLevelId == 1) / summariesCount;

            StatisticData statisticEducationData = new StatisticData
                                                       {
                                                           Titles = new Dictionary<string, string>()
                                                               {
                                                                  {"1","Образование:"},
                                                                  {"2",educationCount.ToString(CultureInfo.InvariantCulture)},
                                                                  {"3","100%"}
                                                               }
                                                       };
            statisticEducationData.Lines = new List<StatisticDataItem>
                                      {
                                          new StatisticDataItem{StrongLevel = 0, Title = "ВПО, чел.", Values = new List<string>{vpoCount.ToString(CultureInfo.InvariantCulture), GetPersentage(vpoCount, educationCount)}},
                                          new StatisticDataItem{StrongLevel = 0, Title = "СПО, чел.", Values = new List<string>{spoCount.ToString(CultureInfo.InvariantCulture), GetPersentage(spoCount, educationCount)}},
                                          new StatisticDataItem{StrongLevel = 0, Title = "СО, чел.", Values = new List<string>{soCount.ToString(CultureInfo.InvariantCulture), GetPersentage(soCount, educationCount)}}
                                      };
            #endregion

            #region Post Data
            int categoryCount = avgPeopleCount;
            int auCount = result.Count(x => x.PostLevelId == 1) / summariesCount;
            int itrisCount = result.Count(x => x.PostLevelId == 2) / summariesCount;
            int orCount = result.Count(x => x.PostLevelId == 3) / summariesCount;
            int vpCount = result.Count(x => x.PostLevelId == 4) / summariesCount;

            StatisticData statisticPostData = new StatisticData
            {
                Titles = new Dictionary<string, string>
                                                               {
                                                                  {"1","Категория::"},
                                                                  {"2",categoryCount.ToString(CultureInfo.InvariantCulture)},
                                                                  {"3","100%"}
                                                               }
            };
            statisticPostData.Lines = new List<StatisticDataItem>
                                      {
                                          new StatisticDataItem{StrongLevel = 0, Title = "АУ, чел.", Values = new List<string>{auCount.ToString(CultureInfo.InvariantCulture), GetPersentage(auCount, categoryCount)}},
                                          new StatisticDataItem{StrongLevel = 0, Title = "ИТРиС, чел.", Values = new List<string>{itrisCount.ToString(CultureInfo.InvariantCulture), GetPersentage(itrisCount, categoryCount)}},
                                          new StatisticDataItem{StrongLevel = 0, Title = "ОР, чел.", Values = new List<string>{orCount.ToString(CultureInfo.InvariantCulture), GetPersentage(orCount, categoryCount)}},
                                          new StatisticDataItem{StrongLevel = 0, Title = "ВП, чел.", Values = new List<string>{vpCount.ToString(CultureInfo.InvariantCulture), GetPersentage(vpCount, categoryCount)}}
                                      };
            #endregion

            return new List<StatisticData>
                       {
                           statisticWorkersData,
                           statisticEducationData,
                           statisticPostData
                       };
        }

        private string GetPersentage(int a, int b)
        {
            return string.Format("{0}%", ((decimal)a / (decimal)b) * 100);
        }
    }
}
