
using System;
using MinStat.Enterprises.WebUI.Validators;

namespace MinStat.Enterprises.WebUI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PersonModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int SummaryId { get; set; }

        [Display(Name = "Вид деятельности")]
        public int ActivityId { get; set; }

        public string ActivityTitle { get; set; }

        [Required(ErrorMessage = "Необходимо указать")]
        [Display(Name = "Код работника")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Необходимо указать")]
        [Display(Name = "Должность")]
        public string Post { get; set; }

        [Required(ErrorMessage = "Необходимо указать")]
        [Display(Name = "Рабочая группа")]
        public int PostLevelId { get; set; }

        public string PostLevel { get; set; }

        [Required(ErrorMessage = "Необходимо указать")]
        [Display(Name = "Уровень образования")]
        public int EducationLevelId { get; set; }

        public string EducationLevel { get; set; }

        [Required(ErrorMessage = "Необходимо указать")]
        [Display(Name = "Годовой доход")]
        public decimal YearSalary { get; set; }

        [Display(Name = "Пол")]
        [UIHint("GenderSelector")]
        public bool Gender { get; set; }

        [Display(Name = "Повышение квалификации в текущем году")]
        public bool WasQualificationIncrease { get; set; }

        [Display(Name = "Аттестация в текущем году")]
        public bool WasValidate { get; set; }

        [Required(ErrorMessage = "Необходимо указать")]
        [RegularExpression("\\d{4}", ErrorMessage = "Укажите ")]
        [YearRange(1940, 18, ErrorMessage = "Укажите правильно")]
        [Display(Name = "Год рождения")]
        public int BirthYear { get; set; }

        [Required(ErrorMessage = "Необходимо указать")]
        [RegularExpression("\\d{4}", ErrorMessage = "Укажите ")]
        [YearRange(1958, 0, ErrorMessage = "Укажите правильно")]
        [Display(Name = "Год приема на работу")]
        public int HiringYear { get; set; }

        [Required(ErrorMessage = "Необходимо указать")]
        [RegularExpression("\\d{4}", ErrorMessage = "Укажите ")]
        [YearRange(1958, 0, ErrorMessage = "Укажите правильно")]
        [Display(Name = "Год вступления в должность")]
        public int StartPostYear { get; set; }

        [Display(Name = "Год увольнения")]
        public int? DismissalYear { get; set; }
    }
}