using Common.Price;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ModelBindingValidation
{
    public class CurrencyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value) =>
            value == null || Enum.IsDefined(typeof(Currency), value);

        public override string FormatErrorMessage(string name) =>
            "Currency is not available.";
    }
}
