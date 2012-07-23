// -----------------------------------------------------------------------
// <copyright file="User.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace MinStat.Enterprises.DAL.POCO
{
  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class User
  {
    public int Id { get; set; }

    public string Mail { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Password { get; set; }

    public int EnterpriseId { get; set; }
  }
}
