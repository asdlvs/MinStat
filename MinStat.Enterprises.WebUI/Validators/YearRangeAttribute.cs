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
        private int _startDate;
        private int _difference;

        public YearRangeAttribute(int startDate, int difference)
        {
            _difference = difference;
            _startDate = startDate;
        }

        public override bool IsValid(object value)
        {
            int yearToTest = (int) value;
            return yearToTest >= _startDate && yearToTest <= DateTime.Now.Year - _difference;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            throw new NotImplementedException();
        }
    }
}