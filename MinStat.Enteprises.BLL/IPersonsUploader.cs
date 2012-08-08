using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinStat.Enterprises.DAL.POCO;

namespace MinStat.Enterprises.BLL
{
    public interface IPersonsUploader
    {
        IEnumerable<Person> ParseFile(byte[] csvFile, int summaryId);
        IEnumerable<Activity> Activities { get; set; }
    }
}
