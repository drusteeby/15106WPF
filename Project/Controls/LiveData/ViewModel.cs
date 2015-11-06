using MCM.Core.Tags;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Tecmosa.Controls.Helpers;

namespace Tecmosa.Controls.LiveData
{
    [Serializable]
    public class ViewModel: DependencyObject
    {

        [XmlIgnoreAttribute]
        public DelegateCommand PlusButtonCommand { get; set; }

        public IRegionManager RegionManager { get; set; } = ServiceLocator.Current.GetInstance<IRegionManager>();


        public ViewModel()
        {
            PlusButtonCommand = new DelegateCommand(OnPlusButtonCommand);
            LiveDataList = new ObservableCollection<LiveDataTagViewModel>();         

        }

        private void OnPlusButtonCommand()
        {
            RegionManager.Regions["WindowRegion"].Context = LiveDataList;
            RegionManager.RegisterViewWithRegion("WindowRegion", typeof(AddData.View));
        }

        public ObservableCollection<LiveDataTagViewModel> LiveDataList
        {
            get { return (ObservableCollection<LiveDataTagViewModel>)GetValue(LiveDataListProperty); }
            set { SetValue(LiveDataListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LiveDataList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LiveDataListProperty =
            DependencyProperty.Register("LiveDataList", typeof(ObservableCollection<LiveDataTagViewModel>), typeof(ViewModel), new UIPropertyMetadata(null));


    }
}
