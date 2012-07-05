// -----------------------------------------------------------------------
// <copyright file="IAdminRepository.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Bonch.Domain.Abstract
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
using Bonch.Domain.POCO;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public interface IAdminRepository
  {
    IEnumerable<FederalDistrict> GetFederalDistricts();

    IEnumerable<FederationSubject> GetFederationSubjects(FederalDistrict federalDistrict);

    IEnumerable<Enterprise> GetEnterprises(FederationSubject federationSubject);

    Enterprise AddEnterprise(Enterprise enterprise);

    void DeleteEnterprise(Enterprise enterprise);

    


  }
}
