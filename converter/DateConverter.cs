using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Registration.Converters
{
    class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTime)
            {
                DateTime dateTime = (DateTime)value;
                return $"{dateTime.Day}/{dateTime.Month}/{dateTime.Year}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = (string)value;
            try
            {
                var cultureInfo = new CultureInfo("en-US");
                return DateTime.Parse((string)value);
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
}
