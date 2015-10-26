using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tecmosa.Results
{
    public static class Model
    {
        //Any tag grouped in the "Results", includes Mins, Maxes, Sigmas, and Results
        private static DataTag[] _results = TagCollection.DataTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result"))).ToArray();
        private static DataTag[] _nonTPSpecific = TagCollection.DataTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result")) && !dt.Name.ToLower().Contains("testpoint")).ToArray();

        //Includes the "Results" virtual tags, which define the base which all the test points are derived from in the XML
        private static DataTag[] _resultHeaderTags = TagCollection.VirtualTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result"))).ToArray();

        public static string[] GetUniqueTestPointNames()
        {
            //Groups the results by the first element after splitting on the period. It then returns an array of the first element in each of the groups.
            return _results.Where(dt => dt.Name.ToLower().Contains("testpoint")).GroupBy(dt => dt.Name.Split('.').First()).Select(grp => grp.First().Name.Split('.').First()).ToArray();
        }

        public static DataTag[] GetTagsByTestPoint(string testPointName)
        {
            //returns an array of DataTag where the name contains the name of the test point
            return _results.Where((dt) => dt.Name.ToLower().Contains(testPointName.ToLower())).ToArray();
        }

        static DataTag GetRelated(string tagName, string relation, bool tpSpecific = true)
        {
            relation = "." + relation;
            if (tpSpecific)
            {
                return _results.SingleOrDefault((dt) =>

                {
                    foreach (string s in tagName.Split('.'))
                        if (!dt.Name.Contains(s)) return false;

                    if (dt.Name.ToLower().Contains(relation.ToLower()))
                        return true;

                    return false;

                });
            }
            else
            {
                return _nonTPSpecific.SingleOrDefault((dt) =>

                {
                    
                    foreach (string s in tagName.Split('.'))
                    {
                        if (s.ToLower().Contains("testpoint")) continue;
                        else if (!dt.Name.Contains(s)) return false;
                    }
                        

                    if (dt.Name.ToLower().Contains(relation.ToLower()))
                        return true;

                    return false;

                });
            }

        }

        public static DataTag GetSigmaTag(string tagName)
        {
            return GetRelated(tagName, "sigma");
        }

        public static DataTag GetMaxTag(string tagName)
        {
            return GetRelated(tagName, "max",false);
        }

        public static DataTag GetMinTag(string tagName)
        {
            return GetRelated(tagName, "min",false);
        }

    }
}
