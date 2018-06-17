using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TechPush.Helper;
using Xamarin.Forms;

namespace TechPush.Converters
{
    public class ImageNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource result;
            var val = (byte[])value;
            if (val != null && val.Length > 0)
            {
                result = ImageSource.FromStream(() => new MemoryStream(val));
            }
            else
            {
                result = ImageSource.FromFile(ResourceHelper.GetImage("noimage.png"));
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}