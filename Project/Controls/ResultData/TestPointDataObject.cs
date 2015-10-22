using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tecmosa.Controls.ResultData
{
    public class TestPointDataObject: DependencyObject
    {
        public TestPointDataObject(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        private ObservableCollection<ResultDataObject> _results;
        public ObservableCollection<ResultDataObject> Results
        {
            get
            {
                if (_results == null) _results = new ObservableCollection<ResultDataObject>();

                return _results;
            }
            set
            {
                _results = value;
            }
        }
    }
}
