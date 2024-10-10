using System;
using System.Globalization;
using Xamarin.Forms;

namespace itsRewards.Extensions.Converters
{
    public class TimeToLocalStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is DateTime)
            {
                var datetime = (DateTime)value;
                return datetime.ToLocalTime().ToString("hh:mm tt");
            }
            else if (value != null && value is TimeSpan)
            {
                var time = DateTime.SpecifyKind(DateTime.Now.Date.Add((TimeSpan)value), DateTimeKind.Utc);
                return time.ToLocalTime().ToString("hh:mm tt");
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