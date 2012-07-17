using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Bonch.Domain.POCO
{
  public class Summary
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreateDate { get; set; }
    public string AuthorName { get; set; }
    public bool Published { get; set; }
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Computed)]
    public int PersonsCount { get; set; }

    [InverseProperty("Summary")]
    public virtual ICollection<SummaryActivity> SummaryActivities { get; set; }
    [ForeignKey("Enterprise")]
    public int EnterpriseId { get; set; }
    [InverseProperty("Summaries")]
    public virtual Enterprise Enterprise { get; set; }
    [InverseProperty("Summary")]
    public virtual ICollection<Person> People { get; set; }


  }
}
