using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonch.WebUI.Models
{
  using System.ComponentModel.DataAnnotations;

  public class LogOnModel
  {
    [Required]
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
  }
}