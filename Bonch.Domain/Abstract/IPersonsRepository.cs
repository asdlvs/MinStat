using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;

namespace Bonch.Domain.Abstract
{
    public interface IPersonsRepository
    {
        IEnumerable<Person> List(Summary summary, Activity activity);
        void Set(Person person);
        void Delete(Person person);
    }
}
