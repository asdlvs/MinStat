using System;
using System.ComponentModel.DataAnnotations;

namespace Bonch.Domain.POCO
{
  public class Person
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
    public string Job { get; set; }
    [NotMapped]
    public DateTime BirthDate { get; set; }
    [ForeignKey("Summary")]
    public int SummaryId { get; set; }
    [InverseProperty("People")]
    public virtual Summary Summary { get; set; }
    [ForeignKey("Activity")]
    public int ActivityId { get; set; }
    [InverseProperty("People")]
    public virtual Activity Activity { get; set; }
    [ForeignKey("EducationLevel")]
    public int EducationLevelId { get; set; }
    [InverseProperty("People")]
    public virtual EducationLevel EducationLevel { get; set; }
    [ForeignKey("JobLevel")]
    public int JobLevelId { get; set; }
    [InverseProperty("People")]
    public virtual JobLevel JobLevel { get; set; }
  }
}
