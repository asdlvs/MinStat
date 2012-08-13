using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using MinStat.Enterprises.DAL.POCO;

namespace MinStat.Enterprises.BLL
{
    public class PersonsUploader : IPersonsUploader
    {
        public IEnumerable<Activity> Activities { get; set; }

        public IEnumerable<Person> ParseFile(byte[] csvFile, int summaryId)
        {
            List<Person> persons = new List<Person>();
            using(MemoryStream ms = new MemoryStream(csvFile))
            {
                using(TextReader reader = new StreamReader(ms, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine())!= null)
                    {
                        string[] array = line.Split(';');
                        Person person = new Person();
                        person.SummaryId = summaryId;
                        string[] activityParts = GetString(array[0]).Split('.');
                        person.ActivityId = this.Activities.First(x =>
                                                                  x.Part_1.ToString(CultureInfo.InvariantCulture) == activityParts[0]
                                                                  && String.Format("{0}{1}", x.Part_2, x.Part_3) == activityParts[1]
                                                                  && String.Format("{0}{1}", x.Part_4, x.Part_5) == activityParts[2]
                            ).Id;
                        person.Title = GetString(array[1]);
                        person.Post = array[2];
                        person.PostLevelId = GetInt(array[3]);
                        person.EducationLevelId = GetInt(array[4]);
                        person.YearSalary = GetDecimal(array[5]);
                        person.Gender = GetBoolean(array[6]);
                        person.WasQualificationIncrease = GetBoolean(array[7]);
                        person.WasValidate = GetBoolean(array[8]);
                        person.BirthYear = GetInt(array[9]);
                        person.HiringYear = GetInt(array[10]);
                        person.StartPostYear = GetInt(array[11]);
                        person.DismissalYear = GetInt(array[12]);
                        persons.Add(person);
                    }
                }
            }
            return persons;
        }

        private string GetString(string element)
        {
            if (!String.IsNullOrWhiteSpace(element))
            {
                return element;
            }
            throw new ArgumentException(element);
        }
        private int GetInt(string element)
        {
            int i;
            if(Int32.TryParse(element, out i))
            {
                return i;
            }
            return 0;
        }
        private decimal GetDecimal(string element)
        {
            decimal d;
            if(Decimal.TryParse(element, out d))
            {
                return d;
            }
            return 0;
        }
        private bool GetBoolean(string element)
        {
            bool b;
            if(Boolean.TryParse(element, out b))
            {
                return b;
            }
            throw new ArgumentException(element);
        }
    }
}
