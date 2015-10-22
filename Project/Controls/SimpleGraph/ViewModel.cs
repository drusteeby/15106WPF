using Prism.Commands;
using Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tecmosa.Controls.SimpleGraph
{
    public class ViewModel: DependencyObject
    {

        public DelegateCommand UpdateGraphCommand { get; set; }

        public ViewModel()
        {
            Line = new ObservableCollection<KeyValuePair<double,double>>();
            Line.Add(new KeyValuePair<double, double>(0, 0));
            Line.Add(new KeyValuePair<double, double>(1, 0.7));
            Line.Add(new KeyValuePair<double, double>(3, 0.7));
            Line.Add(new KeyValuePair<double, double>(4, 0.5));
            Line.Add(new KeyValuePair<double, double>(5, 0.5));
            Line.Add(new KeyValuePair<double, double>(6, 0));
            Line.Add(new KeyValuePair<double, double>(7, 0));

            UpdateGraphCommand = new DelegateCommand(OnUpdateGraph);
        }

        private void OnUpdateGraph()
        {
            //TODO: Make the key-value pairs better, add selectable resolution
            Line.Clear();
            Line.Add(new KeyValuePair<double, double>(1, PullInAmplitude));
            Line.Add(new KeyValuePair<double, double>(1 + PullInDuration, PullInAmplitude));
            Line.Add(new KeyValuePair<double, double>(1 + PullInDuration + 1, HoldInAmplitude));
            Line.Add(new KeyValuePair<double, double>(1 + PullInDuration + 1 + HoldInDuration, HoldInAmplitude));
            Line.Add(new KeyValuePair<double, double>(1 + PullInDuration + 1 + HoldInDuration + 1, 0));
            Line.Add(new KeyValuePair<double, double>(1 + PullInDuration + 1 + HoldInDuration + 2, 0));
        }

        public double PullInAmplitude
        {
            get { return (double)GetValue(PullInAmplitudeProperty); }
            set { SetValue(PullInAmplitudeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PullInAmplitude.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PullInAmplitudeProperty =
            DependencyProperty.Register("PullInAmplitude", typeof(double), typeof(ViewModel), new UIPropertyMetadata(null));




        public double PullInDuration
        {
            get { return (double)GetValue(PullInDurationProperty); }
            set { SetValue(PullInDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PullInDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PullInDurationProperty =
            DependencyProperty.Register("PullInDuration", typeof(double), typeof(ViewModel), new UIPropertyMetadata(null));



        public double HoldInAmplitude
        {
            get { return (double)GetValue(HoldInAmplitudeProperty); }
            set { SetValue(HoldInAmplitudeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoldInCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoldInAmplitudeProperty =
            DependencyProperty.Register("HoldInAmplitude", typeof(double), typeof(ViewModel), new UIPropertyMetadata(null));




        public double HoldInDuration
        {
            get { return (double)GetValue(HoldInDurationProperty); }
            set { SetValue(HoldInDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoldInDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoldInDurationProperty =
            DependencyProperty.Register("HoldInDuration", typeof(double), typeof(ViewModel), new UIPropertyMetadata(null));



        public ObservableCollection<KeyValuePair<double, double>> Line
        {
            get { return (ObservableCollection<KeyValuePair<double, double>>)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Line.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineProperty =
            DependencyProperty.Register("Line", typeof(ObservableCollection<KeyValuePair<double, double>>), typeof(ViewModel), new UIPropertyMetadata(null));


    }
}
