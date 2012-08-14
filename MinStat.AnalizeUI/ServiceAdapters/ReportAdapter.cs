using System.Collections.Generic;
using System.Linq;
using MinStat.AnalizeUI.Models;
using MinStat.AnalizeUI.StatisticDataReference;

namespace MinStat.AnalizeUI.ServiceAdapters
{
    public class ReportAdapter
    {
        public IEnumerable<StatisticDataModel> Convert(IEnumerable<StatisticData> statisticData)
        {
            List<StatisticDataModel> statisticDataModelsCollection = new List<StatisticDataModel>();

            foreach (var data in statisticData)
            {
                StatisticDataModel statisticDataModel = new StatisticDataModel();
                statisticDataModel.Titles = data.Titlesk__BackingField;
                statisticDataModel.Values = data.Linesk__BackingField.Select(x => new StatisticDataItemModel
                {
                    Title = x.Titlek__BackingField,
                    Values = x.Valuesk__BackingField != null ? x.Valuesk__BackingField.ToList() : null,
                    BoldLevel = x.StrongLevelk__BackingField,
                    Id = x.Idk__BackingField
                });
                statisticDataModelsCollection.Add(statisticDataModel);
            }

            return statisticDataModelsCollection;
        }
    }
}
