using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Bonch.WebUI.Models
{


  public class LogOnModel
  {
    [Required(ErrorMessage="Укажите имя пользователя")]
    [Display(Name = "Имя пользователя")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Вы забыли ввести пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }
  }
}