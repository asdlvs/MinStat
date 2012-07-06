using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Bonch.Domain.POCO
{
  using System.ComponentModel.DataAnnotations;

  public class EducationLevel
  {
    public int Id { get; set; }

    public string Title { get; set; }

    [InverseProperty("EducationLevel")]
    public virtual ICollection<Person> People { get; set; }

  }
}
