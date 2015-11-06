using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Tecmosa.Controls.Results.Helpers
{
    public class UseModernConverter : IValueConverter
    {
        DataTemplate ModernTemplate;
        DataTemplate ClassicTemplate;

        public UseModernConverter()
        {
            ModernTemplate = (DataTemplate)Application.Current.Resources["ResultModern"];
            ClassicTemplate = (DataTemplate)Application.Current.Resources["ResultClassic"];
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return ModernTemplate;

            return ClassicTemplate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
