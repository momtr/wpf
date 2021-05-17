using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.Validation
{
    /*
    public class ZeroDivisionRule : ValidationRule
    {
        public string Operation { get; set; }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Operation != "/")
                return ValidationResult.ValidResult;
            int num = 0;
            try
            {
                if (((string)value).Length > 0)
                    num = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "value must be a number");
            }
            if (num == 0)
                return new ValidationResult(false, "number must not be zero");
            return ValidationResult.ValidResult;
        }

    }
    */

    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
    }
    public class NumberOperationCode : DependencyObject
    {
        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(string),
            typeof(NumberOperationCode),
            new PropertyMetadata(default(string))
           );
    }

    public class ZeroDivisionRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (this.NumberOperationCode.Value != "/")
                return ValidationResult.ValidResult;
            int num = 0;
            try
            {
                if (((string)value).Length > 0)
                    num = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "value must be a number");
            }
            if (num == 0)
                return new ValidationResult(false, "number must not be zero");
            return ValidationResult.ValidResult;
        }

        public NumberOperationCode NumberOperationCode { get; set; }

    }
}
