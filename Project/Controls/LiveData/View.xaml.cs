using System;
using System.Collections.Generic;
using System.IO;
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
using Tecmosa.Controls.Helpers;

namespace Tecmosa.Controls.LiveData
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
 

#if DEBUG
            //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(ViewModel));
            //x.Serialize(new FileStream(@"C:\Users\dsteeby\Documents\Projects\HMI\15106 WPF\Project\SampleData\LiveDataViewModelSampleData.xml", FileMode.OpenOrCreate), DataContext);
#endif
        }
    }
}
