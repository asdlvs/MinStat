using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;

namespace Bonch.Domain.Abstract
{
    public interface ISummariesRepository
    {
        List<Summary> List();
        Summary Save(Summary summary, List<Activity> activities, List<Person> people = null);
        void Publish(int summaryId);
        Summary Copy(Summary oldSummaryId, Summary newSummary);
        List<Activity> Activities(Summary summary);
        List<Summary> Undelivered();
    }
}
