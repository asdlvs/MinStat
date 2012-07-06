using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonch.Domain.POCO;
namespace Bonch.Domain.Comparers
{
  public class ActivityEqualityComparer : IEqualityComparer<Activity>
  {
    public bool Equals(Activity x, Activity y)
    {
      return x.Id == y.Id;
    }

    public int GetHashCode(Activity obj)
    {
      return obj.Id;
    }
  }
}
