﻿// -----------------------------------------------------------------------
// <copyright file="FederalDistrict.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Bonch.Domain.POCO
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class FederalDistrict
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public FederationSubject Type { get; set; }
  }
}