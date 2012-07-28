// -----------------------------------------------------------------------
// <copyright file="Filters.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.DAL.HardCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MinStat.DAL.POCO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Filters
    {
        public static IEnumerable<FilterCritery> GetConsolidateCriteries()
        {
            List<FilterCritery> result = new List<FilterCritery>();
            FilterCritery commonCritery = new FilterCritery
            {
                Key = new KeyValuePair<int, string>(1, "<strong>Число работников, всего, чел.</strong>"),
                Inner = new Dictionary<int, string>
                                                        {
                                                            {2, "из них мужчин"},
                                                            {3, "из них женщин"},
                                                            {4, "Принято на работу, чел."},
                                                            {5, "Уволено с работы, чел."},
                                                            {6, "Повысили квалификацию, чел."},
                                                            {7, "Прошли аттестацию, чел."}
                                                        }
            };
            result.Add(commonCritery);

            FilterCritery educationCritery = new FilterCritery
            {
                Key = new KeyValuePair<int, string>(8, "<strong>Образование:</strong>"),
                Inner = new Dictionary<int, string>
                                                                 {
                                                                     {9, "ВПО, чел."},
                                                                     {10, "СПО, чел."},
                                                                     {11, "СО, чел."}
                                                                 }
            };
            result.Add(educationCritery);

            FilterCritery categoryCritery = new FilterCritery
            {
                Key = new KeyValuePair<int, string>(12, "<strong>Категория:</strong>"),
                Inner = new Dictionary<int, string>
                                                     {
                                                         {13, "АУ, чел."},
                                                         {14, "ИТРиС, чел."},
                                                         {15, "ОР, чел."},
                                                         {16, "ВП, чел."},
                                                     }
            };
            result.Add(categoryCritery);

            FilterCritery middleAge = new FilterCritery
            {
                Key = new KeyValuePair<int, string>(17, "<strong>Средний возраст</strong>")
            };
            result.Add(middleAge);

            FilterCritery middleSalary = new FilterCritery
            {
                Key = new KeyValuePair<int, string>(18, "<strong>Среднегодовой доход</strong>")
            };
            result.Add(middleSalary);

            return result;
        }
    }
}
