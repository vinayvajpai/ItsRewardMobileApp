using System;
using System.Globalization;
using Xamarin.Forms;

namespace itsRewards.Extensions.Converters
{
    public class IsNotEmptyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string svalue = (string)value;
                return !string.IsNullOrEmpty(svalue);
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}