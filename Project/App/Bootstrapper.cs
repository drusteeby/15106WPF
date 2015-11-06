using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Prism.Unity;
using MCM.Core.Tags;
using MCM.Core.Comm;
using System;
using MCM.Controls;
using Prism.Regions;

namespace Tecmosa
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            // Load all tags.
            TagCollection.LoadTags("..\\..\\DataTags\\UomDefs.xml"); // This contains parent tags. Needs to be first.
            TagCollection.LoadTags("..\\..\\DataTags\\Alarms.xml");
            TagCollection.LoadTags("..\\..\\DataTags\\Polls.xml");
            TagCollection.LoadTags("..\\..\\DataTags\\Manual.xml");
            TagCollection.LoadTags("..\\..\\DataTags\\System.xml");
            TagCollection.LoadTags("..\\..\\DataTags\\Part.xml");
            TagCollection.LoadTags("..\\..\\DataTags\\Results.xml");

            // Load all settings.
            TagCollection.LoadSettings("SystemEdit");
            TagCollection.LoadSettings("Calibration");
            TagCollection.LoadSettings("PartEdit");

            //Start the Vector Comm
            DataTag vectorEnable = TagCollection.Get("Vector");
            //vectorEnable.ValueChanged += vectorEnable_ValueChanged;

            // Start the PLC Comm.
            DataTag plcIp = TagCollection.Get("PLC");

            FormExtender.MainForm = new MainScreen();

            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }



        private static PlcComm _plcComm;
        static void plcIp_ValueChanged(object sender, EventArgs e)
        {
            // Stop the PLC Comm and restart it with the new IP Address.
            DataTag tag = (DataTag)sender;
            if (_plcComm != null)
                _plcComm.Stop();

            _plcComm = new PlcComm(tag.Name, tag.Text);
            _plcComm.Start();
        }


        private static VectorComm _vectorComm;
        static void vectorEnable_ValueChanged(object sender, EventArgs e)
        {
            DataTag tag = (DataTag)sender;
            DataTag vectorMessage = TagCollection.Get("VectorMessage");
            if (tag.Bool)
            {
                _vectorComm = new VectorComm(vectorMessage.Name);
                vectorMessage.ValueChanged += vectorMessage_ValueChanged;
                VectorComm.CANTransmitTest();
            }
            else
            {
                _vectorComm = null;
                vectorMessage.ValueChanged -= vectorMessage_ValueChanged;
            }
        }

        static void vectorMessage_ValueChanged(object sender, EventArgs e)
        {
            //Vector Message Recieved
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {

            RegionAdapterMappings regionAdapterMappings = Container.TryResolve<RegionAdapterMappings>();

            if (regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof(Window), new WindowRegionAdapter(Container.TryResolve<IRegionBehaviorFactory>()));
            }

            return base.ConfigureRegionAdapterMappings();
        }
    }
}
