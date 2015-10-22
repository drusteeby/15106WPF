using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tecmosa.Controls.ResultData
{
    public class ResultDataObject
    {
        public ResultDataObject(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public double Value { get; set; }
    }
}
