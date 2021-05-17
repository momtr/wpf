using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Registration.Validation
{
    class CharacterValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var val = (string)value;
            if(Regex.Match(val, @"^[A-Za-z]*$").Success)
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "input contains invalid characters");
        }

    }
}
