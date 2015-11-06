using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tecmosa.Controls.Results.ViewModels;

namespace Tecmosa.Controls.Results.Helpers
{
    public class ResultTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate SigmaTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null || item.GetType() != typeof(ResultViewModel)) return DefaultTemplate;

            var vm = (ResultViewModel)item;

            if (vm.HasSigma) return SigmaTemplate;

            return DefaultTemplate;

        }
    }
}
