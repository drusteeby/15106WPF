using MCM.Core.Enum;
using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tecmosa.Controls.Helpers;

namespace Tecmosa.Controls.LiveData
{
    public class LiveDataTagViewModel: DependencyObject
    {
        DataTagNotifyPropertyChanged _tagPropChange;

        public LiveDataTagViewModel() { }


        public LiveDataTagViewModel(DataTag tag)
        {
            _tagPropChange = new DataTagNotifyPropertyChanged(tag);
            var conv = new TagNameConverter();

            _tagPropChange.PropertyChanged += _tagPropChange_PropertyChanged;

            Name = tag.Name.Split('.').Last();
            updateValue();
        }

        void updateValue()
        {
            switch (_tagPropChange.Tag.DataType)
            {
                case DataType.Boolean:
                    Value = _tagPropChange.Tag.Bool.ToString();
                    break;
                case DataType.Byte:
                case DataType.Integer:
                    Value = _tagPropChange.Tag.Int.ToString();
                    break;
                case DataType.Decimal:
                    Value = _tagPropChange.Tag.Double.ToString();
                    break;

                case DataType.String:
                    Value = _tagPropChange.Tag.Text;
                    break;
                default:
                    Value = "Not Found";
                    break;
            }
        }



        private void _tagPropChange_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            updateValue();
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(LiveDataTagViewModel), new UIPropertyMetadata(null));


        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(LiveDataTagViewModel), new UIPropertyMetadata(null));


    }
}
