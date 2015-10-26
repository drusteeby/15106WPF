using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tecmosa.Results
{
    public class StringAddSpaceToCapsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(string)) return null;

            string caps = Regex.Replace((value as string), "[A-Z]", " $0");
            string nums = Regex.Replace(caps, "[0-9]+", " $0");

            return nums;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(string)) return null;

            return Regex.Replace((value as string), "[A-Z][/s+][A-Z]", "$0$1");
        }
    }
}
