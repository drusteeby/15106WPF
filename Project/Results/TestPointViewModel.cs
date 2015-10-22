using MCM.Core.Tags;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Tecmosa.Results
{
    public class TestPointViewModel: DependencyObject
    {
        ListCollectionView _view;
        public DelegateCommand ToggleLiveGrouping { get; set; }
        public TestPointViewModel(string name)
        {
            Name = name;

            Results = new ObservableCollection<ResultViewModel>();

            ToggleLiveGrouping = new DelegateCommand(OnToggleLiveGrouping);

            foreach (DataTag tag in Model.GetTagsByTestPoint(Name))
                Results.Add(new ResultViewModel(tag));

            _view = CollectionViewSource.GetDefaultView(Results) as ListCollectionView;

            _view.IsLiveGrouping = true;
            _view.LiveFilteringProperties.Add("Name");
            _view.GroupDescriptions.Add(new PropertyGroupDescription("Name", new GroupResultByName()));
        }


        private void OnToggleLiveGrouping()
        {
            _view.IsLiveGrouping = !_view.IsLiveGrouping;

            if(_view.IsLiveGrouping == true)
            {
                _view.LiveFilteringProperties.Add("Name");
                _view.GroupDescriptions.Add(new PropertyGroupDescription("Name", new GroupResultByName()));
            }
            else
            {
                _view.LiveFilteringProperties.Clear();
                _view.GroupDescriptions.Clear();
            }
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(TestPointViewModel), new UIPropertyMetadata(null));




        public ObservableCollection<ResultViewModel> Results
        {
            get { return (ObservableCollection<ResultViewModel>)GetValue(ResultsProperty); }
            set { SetValue(ResultsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Results.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultsProperty =
            DependencyProperty.Register("Results", typeof(ObservableCollection<ResultViewModel>), typeof(TestPointViewModel), new UIPropertyMetadata(null));





    }
}
