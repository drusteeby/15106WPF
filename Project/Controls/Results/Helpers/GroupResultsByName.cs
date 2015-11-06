using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tecmosa.Controls.Results.Helpers
{
    public class GroupResultByName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> tp = new List<String>((value as string).Split('.'));
            string[] substringList = { "Main", "CCPilot", "Pilot", "Post" };

            foreach (string split in tp)
                foreach (string sub in substringList)
                {
                    string r = GetSubStringOrDefault(split, sub);
                    if (r != null) return r;
                }

            return tp.Last();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string GetSubStringOrDefault(string s, string containing)
        {
            var contLower = containing.ToLower();
            var sLower = s.ToLower();

            if (sLower.Contains(contLower))
            {
                int index = sLower.IndexOf(contLower);
                return s.Substring(0, index);
            }

            return null;
        }
    }
}
