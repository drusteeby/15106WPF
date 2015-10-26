using MCM.Controls;
using MCM.Core.Controls;
using MCM.Core.Logging;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Prism.Regions;
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
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tecmosa.Results;
using Winforms = System.Windows.Forms;

namespace Tecmosa
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    /// 




    public partial class Shell : Window
    {
        double _mainScreenWidth;
        double _mainScreenHeight;

        public DelegateCommand<KeyEventArgs> KeyPressCommand { get; set; }



        public Shell(IRegionManager RegionManager)
        {
            InitializeComponent();

            FormExtender.MainForm = mainScreen;
            System.Windows.Forms.Application.Run(FormExtender.MainForm);

            RegionManager.RegisterViewWithRegion("MainRegion", typeof(Results.ResultPage));

            KeyPressCommand = new DelegateCommand<KeyEventArgs>(OnKeyPressed);
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)KeyPressed);


            //transform.ScaleX = 1;
            //transform.ScaleY = 1;

            //_mainScreenWidth = winFormsHost.Width = mainScreen.Width;
            //_mainScreenHeight = winFormsHost.Height = mainScreen.Height;


            MemoryStream ms = new MemoryStream();
            mainScreen.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();

            mainGrid.Background = new ImageBrush(bi);
            this.Focus();

        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            OnKeyPressed(e);
        }

        private void OnKeyPressed(KeyEventArgs e)
        {
            if ((e.Key == Key.F5) && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                DataSimulator simulator = new DataSimulator();
                mainScreen.AddOwnedForm(simulator);
                simulator.Visible = true;
                Log.Add(Log.Type.UserAction, "User opened simulation dialog.");
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // if (double.IsNaN(Application.Current.MainWindow.Width) || double.IsNaN(Application.Current.MainWindow.Height)) return;

           // transform.ScaleX = Application.Current.MainWindow.Width / _mainScreenWidth;
           // transform.ScaleY = Application.Current.MainWindow.Height / _mainScreenHeight;


        }

        private void WindowsFormsHost_LayoutError(object sender, System.Windows.Forms.Integration.LayoutExceptionEventArgs e)
        {
            e.ThrowException = false;            
        }
    }
}
