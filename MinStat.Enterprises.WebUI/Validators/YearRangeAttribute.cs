using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MinStat.Enterprises.WebUI.Validators
{
    public class YearRangeAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly int _startDate;
        private readonly int _difference;

        public YearRangeAttribute(int startDate, int difference)
        {
            _difference = difference;
            _startDate = startDate;
        }

        public override bool IsValid(object value)
        {
            int yearToTest = (int)value;
            return yearToTest >= _startDate && yearToTest <= DateTime.Now.Year - _difference;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(this.ErrorMessage),
                ValidationType = "range",
            };
            rule.ValidationParameters.Add("min", _startDate);
            rule.ValidationParameters.Add("max", DateTime.Now.Year - _difference);
            yield return rule;
        }
    }
}