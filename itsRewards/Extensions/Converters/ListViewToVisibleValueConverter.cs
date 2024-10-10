using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace itsRewards.Extensions.Converters
{
    public class ListViewToVisibleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var data = value as IEnumerable<object>;
            return data != null && data.Any();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
