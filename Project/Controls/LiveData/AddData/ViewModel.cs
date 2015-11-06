using MCM.Core.Tags;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tecmosa.Controls.LiveData.AddData
{
    public class ViewModel: DependencyObject
    {
        ObservableCollection<LiveDataTagViewModel> DataList;
        public IRegionManager RegionManager { get; set; } = ServiceLocator.Current.GetInstance<IRegionManager>();

    

        public ViewModel()
        {
            DataTagList = new ObservableCollection<DataTag>(TagCollection.DataTags);
            DataList = (ObservableCollection < LiveDataTagViewModel >)RegionManager.Regions["WindowRegion"].Context;
            SelectedItems = new ObservableCollection<DataTag>();
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;

        }

        private void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
           
        }

        public ObservableCollection<DataTag> SelectedItems
        {
            get { return (ObservableCollection<DataTag>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(ObservableCollection<DataTag>), typeof(ViewModel), new UIPropertyMetadata());



        public ObservableCollection<DataTag> DataTagList
        {
            get { return (ObservableCollection<DataTag>)GetValue(DataTagListProperty); }
            set { SetValue(DataTagListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataTagList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataTagListProperty =
            DependencyProperty.Register("DataTagList", typeof(ObservableCollection<DataTag>), typeof(ViewModel), new UIPropertyMetadata(null));




    }
}
