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
    public class GroupBoxHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string header = (value as string).Split('.').Last().ToLower();

            if (header.Contains("ccpilot")) return "CC Pilot";
            else if (header.Contains("pilot")) return "Pilot";
            else if (header.Contains("main")) return "CC Pilot";
            else if (header.Contains("post")) return "Post";
            else return Regex.Replace((value as string).Split('.').Last(), "[A-Z]", " $0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
