using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Tecmosa.Results
{
    public class ResultViewModel: DependencyObject
    {
        DataTagNotifyPropertyChanged _resultTag;
        DataTag _resultSigma;
        DataTag _resultMax;
        DataTag _resultMin;

        public ResultViewModel(DataTag tag)
        {
            _resultTag = new DataTagNotifyPropertyChanged(tag);
            _resultTag.PropertyChanged += _resultTag_PropertyChanged;

            Name = tag.Name;
            Value = tag.Double;

        }

        private void _resultTag_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Value = _resultTag.Tag.Double;
        }



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ResultViewModel), new UIPropertyMetadata(null));



        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ResultViewModel), new UIPropertyMetadata(null));



        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ResultViewModel), new UIPropertyMetadata(null));






    }
}
