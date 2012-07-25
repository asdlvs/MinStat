using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinStat.Enterprises.WebUI.Models
{
  public class ErrorModel
  {
    /// <summary>
    /// Gets or sets the Type of Exception
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the Message of Exception
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the Stack Trace of Exception
    /// </summary>
    public string StackTrace { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether show or not the stacktrace
    /// </summary>
    public bool ShowStackTrace { get; set; }

    /// <summary>
    /// Gets or sets the ViewName for this model
    /// </summary>
    public string ViewName { get; set; }
  }
}