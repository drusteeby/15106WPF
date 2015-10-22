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
    /// Interaction logic for ScalableView.xaml
    /// </summary>
    public partial class ScalableView : UserControl
    {
        static ScalableView()
        {
            ViewProperty = DependencyProperty.Register("View", typeof(Visual), typeof(ScalableView), new PropertyMetadata(null));
        }



        public Visual View
        {
            get { return (Visual)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for View.  This enables animation, styling, binding, etc...
        public static DependencyProperty ViewProperty;
            

        
    }
}
