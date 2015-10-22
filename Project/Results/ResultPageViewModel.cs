using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tecmosa.Results
{
    public class ResultPageViewModel: DependencyObject
    {


        public ResultPageViewModel()
        {
            TestPoints = new ObservableCollection<TestPointViewModel>();

            foreach (string testPointName in Model.GetUniqueTestPointNames())
                TestPoints.Add(new TestPointViewModel(testPointName));


        }



        public ObservableCollection<TestPointViewModel> TestPoints
        {
            get { return (ObservableCollection<TestPointViewModel>)GetValue(TestPointsProperty); }
            set { SetValue(TestPointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestPoints.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestPointsProperty =
            DependencyProperty.Register("TestPoints", typeof(ObservableCollection<TestPointViewModel>), typeof(ResultPageViewModel), new UIPropertyMetadata(null));




    }
}
