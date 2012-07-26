// -----------------------------------------------------------------------
// <copyright file="Person.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace MinStat.DAL.POCO
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class Person
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public string Post { get; set; }

    public int PostLevelId { get; set; }

    public int EducationLevelId { get; set; }

    public decimal YearSalary { get; set; }

    public bool Gender { get; set; }

    public bool WasQualificationIncrease { get; set; }

    public bool WasValidate { get; set; }

    public int BirthYear { get; set; }

    public int HiringYear { get; set; }

    public int StartPostYear { get; set; }

    public int? DismissalYear { get; set; }


    public int SummaryId { get; set; }


    public int ActivityId { get; set; }


  }
}
