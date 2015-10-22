using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Tecmosa.Controls.ResultData
{
    /// <summary>
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView : UserControl
    {
        //TODO: Seperate this
        DataTag[] _results = TagCollection.DataTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result"))).ToArray();
        DataTag[] _resultHeaderTags = TagCollection.VirtualTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result"))).ToArray();
        DataTag[] _resultValueTags = TagCollection.DataTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result"))).ToArray();

        public GridView(GridViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
            
            
           /* var definedTestPoints = _resultValueTags.Where(dt => dt.Name.ToLower().Contains("testpoint")).GroupBy(dt => dt.Name.Split('.').First()).Select(grp => grp.First().Name.Split('.').First());
            //var q2 = _resultValueTags.Where(dt => Regex.Match(dt.Name.Split('.').Single((s) => s.Contains("test")), @"(testpoint)[\d+]", RegexOptions.IgnoreCase).Success);

            //Add the test point with a header value of it's number.

            foreach (string tp in definedTestPoints)
            {

                var col = new DataGridTemplateColumn();
                col.CellTemplate = (DataTemplate)Application.Current.Resources["headerCellTemplate"];
                col.HeaderTemplate = (DataTemplate)Application.Current.Resources["headerCellTemplate"];               
                
               // resultsGrid.Columns.Add(col);
            }

            foreach (string tp in definedTestPoints)
            {

                var col = new DataGridTemplateColumn();
                col.HeaderTemplate = (DataTemplate)Application.Current.Resources["headerCellTemplate"];

                
                //resultsGrid.Columns.Add(col);
            }
            */
        }


















        //Here is where we add the custom styles for the columns
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
                templateColumn.Header = String.Empty;
                templateColumn.CellTemplate = (DataTemplate)Application.Current.Resources["headerCellTemplate"];
               

                e.Column = templateColumn;
            }
        }

        private void Grid_LayoutUpdated(object sender, EventArgs e)
        {
            
            var tempWidth = this.Width;
            this.Width = this.Height;
            this.Height = tempWidth;
        }
    }
}
