using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Registration.Validation
{
    class EmptyValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var val = (string)value;
            if(val != null && val.Length > 0)
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "input must contain one or more characters");
        }

    }
}
