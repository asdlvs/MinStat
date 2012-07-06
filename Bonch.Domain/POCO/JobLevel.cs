using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Bonch.Domain.POCO
{
  public class JobLevel
  {
    public int Id { get; set; }

    public string Title { get; set; }

    [InverseProperty("JobLevel")]
    public virtual ICollection<Person> People { get; set; }
  }
}
