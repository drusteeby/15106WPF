using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tecmosa.Sequences
{
    public static class Results
    {
        private static DataTag[] _results;

        public static void Start()
        {
            _results = TagCollection.DataTags.Where((dt) => dt.Group.Contains("Results") || dt.Group.Contains("Sigmas")).ToArray();
            clearResults();
        }

        public static void clearResults()
        {
            foreach (DataTag tag in _results)
                tag.Double = Double.NaN;
        }
    }
}
