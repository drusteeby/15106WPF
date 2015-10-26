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
    [Serializable]
    public class ResultViewModel: DependencyObject
    {
        DataTagNotifyPropertyChanged _resultTag;
        DataTag _resultSigma;
        DataTagNotifyPropertyChanged _sigmaPropertyChanged;
        DataTag _resultMax;
        DataTagNotifyPropertyChanged _maxPropertyChanged;
        DataTag _resultMin;
        DataTagNotifyPropertyChanged _minPropertyChanged;

        public bool HasMax { get; set; }
        public bool HasMin { get; set; }
        public bool HasSigma { get; set; }

        public ResultViewModel() { } //default parameterless constructor

        public ResultViewModel(DataTag tag)
        {
            _resultTag = new DataTagNotifyPropertyChanged(tag);
            _resultTag.PropertyChanged += _resultTag_PropertyChanged;

            Name = tag.Name;
            Value = tag.Double;

            _resultSigma = Model.GetSigmaTag(tag.Name);
            _resultMax = Model.GetMaxTag(tag.Name);
            _resultMin = Model.GetMinTag(tag.Name);

            if (HasSigma = !(_resultSigma == null))
            {
                _sigmaPropertyChanged = new DataTagNotifyPropertyChanged(_resultSigma);
                _sigmaPropertyChanged.PropertyChanged += _sigmaPropertyChanged_PropertyChanged;
            }

            if (HasMax = !(_resultMax == null))
            {
                _maxPropertyChanged = new DataTagNotifyPropertyChanged(_resultMax);
                _maxPropertyChanged.PropertyChanged += _maxPropertyChanged_PropertyChanged;
            }

            if (HasMin = !(_resultMin == null))
            {
                _minPropertyChanged = new DataTagNotifyPropertyChanged(_resultMin);
                _minPropertyChanged.PropertyChanged += _minPropertyChanged_PropertyChanged;
            }
        }

        private void _minPropertyChanged_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MinValue = _minPropertyChanged.Tag.Double;
        }

        private void _maxPropertyChanged_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MaxValue = _maxPropertyChanged.Tag.Double;
        }

        private void _sigmaPropertyChanged_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SigmaValue = _sigmaPropertyChanged.Tag.Double;
        }

        private void _resultTag_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Value = _resultTag.Tag.Double;

            if (double.IsNaN(Value)) PassFail = null;
            else if (HasMin && Value < MinValue) PassFail = false;
            else if (HasMax && Value > MaxValue) PassFail = false;
            else PassFail = true;
        }


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


        public double SigmaValue
        {
            get { return (double)GetValue(SigmaValueProperty); }
            set { SetValue(SigmaValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SigmaValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SigmaValueProperty =
            DependencyProperty.Register("SigmaValue", typeof(double), typeof(ResultViewModel), new UIPropertyMetadata(null));


        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(ResultViewModel), new UIPropertyMetadata(null));


        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(ResultViewModel), new UIPropertyMetadata(null));




        public bool? PassFail
        {
            get { return (bool?)GetValue(PassFailProperty); }
            set { SetValue(PassFailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PassFail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassFailProperty =
            DependencyProperty.Register("PassFail", typeof(bool?), typeof(ResultViewModel), new UIPropertyMetadata(null));



    }
}
