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
    class PasswordValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var val = (string)value;
            if (val != null && val.Length >= 8 && Regex.Match(val, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})").Success)
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "input must contain lc, uc, numbers, and special characters");
        }
    }
}
