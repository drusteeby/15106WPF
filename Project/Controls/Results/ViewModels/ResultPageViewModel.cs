using MCM.Core.Tags;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tecmosa.Controls.Results;

namespace Tecmosa.Controls.Results.ViewModels
{
    [Serializable]
    public class ResultPageViewModel : DependencyObject
    {
        public DelegateCommand ToggleGroupingCommand { get; set; }
        public DelegateCommand ToggleViewCommand { get; set; }

        public ResultPageViewModel()
        {
            TestPoints = new ObservableCollection<TestPointViewModel>();

            ToggleGroupingCommand = new DelegateCommand(OnToggleGroupingCommand);
            ToggleViewCommand = new DelegateCommand(OnToggleViewCommand);

            foreach (string testPointName in Model.GetUniqueTestPointNames())
                TestPoints.Add(new TestPointViewModel(testPointName));

            UseModern = false;

        }

        private void OnToggleViewCommand()
        {
            UseModern = !UseModern;
        }

        private void OnToggleGroupingCommand()
        {
            foreach (TestPointViewModel tpvm in TestPoints)
                tpvm.ToggleLiveGrouping();
        }

        public ObservableCollection<TestPointViewModel> TestPoints
        {
            get { return (ObservableCollection<TestPointViewModel>)GetValue(TestPointsProperty); }
            set { SetValue(TestPointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestPoints.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestPointsProperty =
            DependencyProperty.Register("TestPoints", typeof(ObservableCollection<TestPointViewModel>), typeof(ResultPageViewModel), new UIPropertyMetadata(null));





        public bool UseModern
        {
            get { return (bool)GetValue(UseModernProperty); }
            set { SetValue(UseModernProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UseModern.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseModernProperty =
            DependencyProperty.Register("UseModern", typeof(bool), typeof(ResultPageViewModel), new UIPropertyMetadata(null));


    }
}
