using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bonch.WebUI.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Login { get; set; }
        [Required(ErrorMessage="Укажите ваше имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Укажите вашу фамилию")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Укажите номер вашего телефона")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        public string EnterpriseId { get; set; }
        public string EnterpriseName { get; set; }
    }
}