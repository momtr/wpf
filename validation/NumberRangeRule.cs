using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Calculator.Validation
{
    class NumberRangeRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public NumberRangeRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int num = 0;
            try
            {
                if (((string)value).Length > 0)
                    num = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "please inout a valid number");
            }
            if ((num < Min) || (num > Max))
            {
                return new ValidationResult(false, "number out of bounds");
            }
            return ValidationResult.ValidResult;
        }

    }
}
