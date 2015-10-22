using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Tecmosa.DataObjects;
using System.Globalization;

namespace Tecmosa.Controls.ResultData
{
    public class TestPointNameGrouper : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tp = (value as string).Split('.').First();

            if (tp.ToLower().Contains("testpoint"))
                return tp;
            else
                return "Default";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ResultNameGrouper : IValueConverter
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


    public class GridViewModel: DependencyObject
    {
        DataTag[] _results = TagCollection.DataTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result"))).ToArray();
        DataTag[] _resultHeaderTags = TagCollection.VirtualTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result"))).ToArray();
        DataTag[] _resultValueTags = TagCollection.DataTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result"))).ToArray();

        ICollectionView _view;
        public GridViewModel()
        {
            ResultData = new ObservableCollection<TestPointDataObject>();      

            
            //get the unique test points
            var definedTestPoints = _resultValueTags.Where(dt => dt.Name.ToLower().Contains("testpoint")).GroupBy(dt => dt.Name.Split('.').First()).Select(grp => grp.First().Name.Split('.').First());
            //var q2 = _resultValueTags.Where(dt => Regex.Match(dt.Name.Split('.').Single((s) => s.Contains("test")), @"(testpoint)[\d+]", RegexOptions.IgnoreCase).Success);

            //Add the test point with a header value of it's number.
            foreach (string tp in definedTestPoints)
                ResultData.Add(new TestPointDataObject(Regex.Match(tp,@"[\d+]").Value));

            foreach (DataTag tag in _resultValueTags.Where(dt => dt.Name.ToLower().Contains("testpoint")))
                ResultData.ElementAt(Convert.ToInt32(Regex.Match(tag.Name, @"[\d+]").Value) - 1).Results.Add(new ResultDataObject(tag.Name,tag.Double));

            foreach (DataTag tag in _results)
            {
                tag.ValueChanged += Tag_ValueChanged;
                tag.ValueSet += Tag_ValueSet;
            }

            foreach (var v in ResultData)
            {
                
                _view = CollectionViewSource.GetDefaultView(v.Results);
                _view.GroupDescriptions.Add(new PropertyGroupDescription("Name", new ResultNameGrouper()));
            }

        }

        private void Tag_ValueSet(object sender, EventArgs e)
        {
            
        }

        private void Tag_ValueChanged(object sender, EventArgs e)
        {
           
        }



        public ObservableCollection<DataTag> Results
        {
            get { return (ObservableCollection<DataTag>)GetValue(ResultsProperty); }
            set { SetValue(ResultsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Results.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultsProperty =
            DependencyProperty.Register("Results", typeof(ObservableCollection<DataTag>), typeof(GridViewModel), new UIPropertyMetadata(null));




        public ObservableCollection<TestPointDataObject> ResultData
        {
            get { return (ObservableCollection<TestPointDataObject>)GetValue(ResultDataProperty); }
            set { SetValue(ResultDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResultData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultDataProperty =
            DependencyProperty.Register("ResultData", typeof(ObservableCollection<TestPointDataObject>), typeof(GridViewModel), new UIPropertyMetadata(null));



    }
}
