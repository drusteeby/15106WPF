using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tecmosa
{
    public class DataTagNotifyPropertyChanged : INotifyPropertyChanged
    {
        public DataTag Tag { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public DataTagNotifyPropertyChanged(DataTag tag)
        {
            Tag = tag;
            Tag.ValueChanged += _tag_ValueChanged;
            Tag.ValueSet += _tag_ValueChanged;
        }

        private void _tag_ValueChanged(object sender, EventArgs e)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Tag"));
        }
    }
}
