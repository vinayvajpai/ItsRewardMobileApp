using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace itsRewards.Extensions.Converters
{
    public class ImageSourceFromStreamConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            var source = value as string;

            if (string.IsNullOrEmpty(source))
                return null;

            Task.Run(async() =>
            {
                var decodedUrl = HttpUtility.UrlDecode(source, Encoding.UTF8);
                var byteImage = DownlodaImage(decodedUrl);
                var stream = new MemoryStream(byteImage);
                return ImageSource.FromStream(() => stream);
            });
            return null;
        }

        byte[] DownlodaImage(string url)
        {
            try
            {

            
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(url);
                return data;
            }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}

