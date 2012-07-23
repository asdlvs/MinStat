// -----------------------------------------------------------------------
// <copyright file="Summary.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.Enterprises.DAL.POCO
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class Summary
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime CreateDate { get; set; }

    public string AuthorName { get; set; }

    public bool Published { get; set; }

    public int EnterpriseId { get; set; }

    public DateTime PublishedDate { get; set; }
  }
}
