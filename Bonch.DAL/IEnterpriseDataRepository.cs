﻿// -----------------------------------------------------------------------
// <copyright file="IEnterpriseDataRepository.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.Enterprises.DAL
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using MinStat.Enterprises.DAL.POCO;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public interface IEnterpriseDataRepository
  {
    void CreateSummary(int enterpriseId, string title, List<int> activitiesIds);

    void CopySummary(int enterpriseId, string title, int summaryId);

    void RemoveSummary(int summaryId);

    void AddActivities(int summaryId, List<int> activitiesIds);

    void RemoveActivity(int summaryId, int activitiId);

    void CreatePerson(int summaryId,
      int activityId,
      string title,
      string post,
      int postLevelId,
      int educationLevelId,
      decimal YearSalary,
      bool Gender,
      bool WasQualificationIncrease,
      bool WasValidate,
      int BirthYear,
      int HiringYear,
      int StartPostYear,
      int DismissalYear);

    void UpdatePerson(int personId, string title, string post, int postLevelId, int educationLevelId, decimal yearSalary, bool gender, bool wasQualificationIncrease,
      bool wasValidate, int birthYear, int hiringYear, int startPostYear, int dismissalYear);

    void RemovePerson(int personId);

    List<Person> FindPerson(string searchText);

  }
}