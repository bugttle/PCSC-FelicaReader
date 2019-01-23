using System;
using System.Windows.Data;

namespace PCSC_FelicaReader.ViewModels.Converters
{
    public class RunningStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is bool)) throw new ArgumentException("should be boolean", "value");
            return ((bool?)value == true) ? "Running" : "Stopping";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
