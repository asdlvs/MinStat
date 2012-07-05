// -----------------------------------------------------------------------
// <copyright file="User.cs" company="Microsoft">
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
  [Serializable]
  public class User
  {
    public int Id { get; set; }

    public string Mail { get; set; }

    public Enterprise Enterprise { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Login
    {
      get
      {
        return this.Mail;
      }
      set
      {
        this.Mail = value;
      }

    }
  }
}
