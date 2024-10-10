using System;
using System.Globalization;
using Xamarin.Forms;

namespace itsRewards.Extensions.Converters
{
    public class DatetimeToLocalStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is DateTime)
            { 
                var datetime = (DateTime)value;
                return datetime.ToLocalTime().ToString("dddd, dd MMMM yyyy");
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}