using MCM.Core.Enum;
using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tecmosa.Controls.Helpers
{
    public class TagDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataTag tag = (DataTag)value;
            string retString = String.Empty;

            switch(tag.DataType)
            {
                case DataType.Boolean:
                    retString = tag.Bool.ToString();
                    break;
                case DataType.Byte:
                case DataType.Integer:
                    retString = tag.Int.ToString();
                    break;
                case DataType.Decimal:
                    retString = tag.Double.ToString();
                    break;              

                case DataType.String:
                    retString = tag.Text;
                    break;
                default:
                    retString = "Not Found";
                    break;
            }

            return retString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
