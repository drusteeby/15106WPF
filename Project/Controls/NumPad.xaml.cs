using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tecmosa.Controls
{
    /// <summary>
    /// Interaction logic for NumPad.xaml
    /// </summary>
    public partial class NumPad : UserControl
    {
        public DelegateCommand<string> ButtonPressedCommand { get; set; }
        
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public NumPad()
        {
            ButtonPressedCommand = new DelegateCommand<string>(OnButtonPressedCommand);
            InitializeComponent();
        }

        private void OnButtonPressedCommand(string keyStr)
        {
            var target = (UIElement)RegionManager.Regions["WindowRegion"].Context;
            var routedEvent = TextCompositionManager.TextInputEvent;
            KeyConverter kc = new KeyConverter();

            target.RaiseEvent(
                             new TextCompositionEventArgs(
                               InputManager.Current.PrimaryKeyboardDevice,
                               new TextComposition(InputManager.Current, target, keyStr))
                             { RoutedEvent = routedEvent }
                           );

        }
    }
}
