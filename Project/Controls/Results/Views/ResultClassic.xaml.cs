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

namespace Tecmosa.Controls.Results.Views
{
    /// <summary>
    /// Interaction logic for ResultClassic.xaml
    /// </summary>
    public partial class ResultClassic : UserControl
    {
        LinearGradientBrush _greenBG = new LinearGradientBrush(Color.FromRgb(86, 240, 86), Color.FromRgb(22, 176, 22), new Point(0, 0), new Point(0, 1));
        LinearGradientBrush _redBG = new LinearGradientBrush(Color.FromRgb(240, 86, 86), Color.FromRgb(176, 22, 22), new Point(0, 0), new Point(0, 1));
        SolidColorBrush _whiteBG = new SolidColorBrush(Colors.White);

        public ResultClassic()
        {
            InitializeComponent();
        }

        public void SetRedBackground(bool sigma = false)
        {
            if (!sigma) resultBorder.Background = _redBG;
            else sigmaBorder.Background = _redBG;

            SetValue(Grid.ZIndexProperty, 5);
        }

        public void SetGreenBackground(bool sigma = false)
        {
            if (!sigma) resultBorder.Background = _greenBG;
            else sigmaBorder.Background = _greenBG;

            SetValue(Grid.ZIndexProperty, 5);
        }

        public void SetWhiteBackground(bool sigma = false, int zIndex = 0)
        {
            if (!sigma) resultBorder.Background = _whiteBG;
            else
            {
                sigmaBorder.Background = _whiteBG;
            }

            SetValue(Grid.ZIndexProperty, zIndex);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SigmaLine.Y2 = this.ActualHeight + 3;
        }
    }
}
