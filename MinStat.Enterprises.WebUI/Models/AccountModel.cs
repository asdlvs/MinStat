using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MinStat.Enterprises.WebUI.Models
{
    public class AccountModel
    {
        [Display(Name = "Адрес электронной почты")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Пароль")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}