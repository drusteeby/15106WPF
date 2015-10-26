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
using System.Xml;
using Tecmosa._Common.ExtensionMethods;

namespace Tecmosa.Results
{
    /// <summary>
    /// Interaction logic for ResultPage.xaml
    /// </summary>
    public partial class ResultPage : UserControl
    {
        public ResultPage(ResultPageViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;

#if DEBUG
            //save sample data for blend
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(vm.GetType());
            TextWriter writer = new StreamWriter("../../SampleData/ResultPageViewModelSampleData.xml");
            x.Serialize(writer, vm);
            Console.WriteLine();
            Console.ReadLine();

#endif
        }

    }
}
