using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace Bonch.WebUI.Models
{
  public class PersonViewModel
  {
    [HiddenInput(DisplayValue = false)]
    public string Id { get; set; }

    [HiddenInput(DisplayValue = false)]
    public string SummaryId { get; set; }

    [HiddenInput(DisplayValue = true)]
    [Display(Name = "Блок")]
    public string Summary { get; set; }

    [HiddenInput(DisplayValue = false)]
    public string ActivityId { get; set; }
    
    [HiddenInput(DisplayValue = true)]
    [Display(Name = "Активность")]
    public string Activity { get; set; }

    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Необходимо указать имя.")]
    public string FirstName { get; set; }
    
    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Необходимо указать фамилию.")]
    public string LastName { get; set; }
    
    [Display(Name = "Зарплата")]
    [Required(ErrorMessage = "Необходимо указать заработную плату.")]
    public string Salary { get; set; }
    
    [Display(Name = "Должность")]
    [Required(ErrorMessage = "Необходимо указать должность.")]
    public string Job { get; set; }
    
    [Display(Name = "Возраст")]
    [Required(ErrorMessage = "Необходимо указать дату рождения.")]
    public string Age { get; set; }
    
    [Display(Name = "Уровень образования")]
    public string EducationLevel { get; set; }
    
    [Display(Name = "Уровень должности")]
    public string JobLevel { get; set; }
  }
}