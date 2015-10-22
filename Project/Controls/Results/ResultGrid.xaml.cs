using MCM.Core.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tecmosa.Controls.Results
{
    /// <summary>
    /// Interaction logic for ResultGrid.xaml
    /// </summary>
    public partial class ResultGrid : UserControl
    {
        DataTag[] _resultHeaderTags;
        DataTag[] _resultValueTags;
        DataTag[] _resultSigmaTags;
        DataTag curTestPoint;

        List<List<ResultCell>> _cells;
        int numCols = 0;
        int numRows = 0;

        Rectangle _highlight;

        public ResultGrid()
        {
            InitializeComponent();
        }

        public void Load()
        {
            _resultHeaderTags = TagCollection.VirtualTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result"))).ToArray();
            _resultValueTags = TagCollection.DataTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("result"))).ToArray();
            _resultSigmaTags = TagCollection.DataTags.Where((dt) => dt.Group.Any((gr) => gr.ToLower().Contains("sigma"))).ToArray();

            _cells = new List<List<ResultCell>>();



            //getting the number of columns
            foreach (DataTag tag in _resultValueTags)
            {
                //number of columns is equal to the highest number in the 'testpointX' data tags

                var tpNum = Regex.Match(tag.Name, @"[\d+]");
                if (tpNum.Success)
                    numCols = Math.Max(numCols, Convert.ToByte(tpNum.Value));
            }

            //add one row to the grid for the column headers
            var rowOne = new RowDefinition();
            rowOne.Name = "columnHeaders";
            rowOne.Height = GridLength.Auto;
            resultsGrid.RowDefinitions.Add(rowOne);
            numRows++;

            //add one column to the grid for the row headers
            var colOne = new ColumnDefinition();
            colOne.Width = GridLength.Auto;
            resultsGrid.ColumnDefinitions.Add(colOne);
            numCols++;

            //adding the columns to the grid
            for (int i = 1; i < numCols; i++)
            {
                var col = new ColumnDefinition();
                col.Name = "Header" + i.ToString();
                resultsGrid.ColumnDefinitions.Add(col);
                ResultsHeader headerBox = new ResultsHeader();
                headerBox.HeaderText = i.ToString();

                DataGridColumnHeader columnHeader = new DataGridColumnHeader();
                columnHeader.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                columnHeader.VerticalContentAlignment = VerticalAlignment.Stretch;
               
                columnHeader.Content = headerBox;


                Grid.SetColumn(columnHeader, i);
                Grid.SetRow(columnHeader, 0);
                
                resultsGrid.Children.Add(columnHeader);
            }


            //adding the rows and row headers
            foreach (DataTag tag in _resultHeaderTags)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = GridLength.Auto;
                rowDef.Name = tag.Name.Split('.').Last(); //So I can find it later, regex removes all non alpha chars
                resultsGrid.RowDefinitions.Add(rowDef);

                ResultsHeader headerBox = new ResultsHeader();
                headerBox.HeaderText = tag.Description;
                //headerBox.headerText.HorizontalAlignment = HorizontalAlignment.Left;
                headerBox.HorizontalAlignment = HorizontalAlignment.Stretch;

                DataGridRowHeader rowHeader = new DataGridRowHeader();
                rowHeader.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                rowHeader.VerticalContentAlignment = VerticalAlignment.Stretch;
                rowHeader.HorizontalAlignment = HorizontalAlignment.Stretch;

                rowHeader.Content = headerBox;

                Grid.SetRow(rowHeader, resultsGrid.RowDefinitions.Count - 1);
                Grid.SetColumn(rowHeader, 0);
                resultsGrid.Children.Add(rowHeader);
                numRows++;
            }


            //adding the result cells
            for (int row = 0; row < numRows; row++)
            {
                if (row == 0)
                {
                    _cells.Add(null);
                    continue;
                }
                else _cells.Add(new List<ResultCell>());

                for (int col = 0; col < numCols; col++)
                {
                    if (col == 0)
                    {
                        _cells[row].Add(null);
                        continue;
                    }

                    var cell = new ResultCell();

                    Grid.SetColumn(cell, col);
                    Grid.SetRow(cell, row);

                    _cells[row].Add(cell);
                    resultsGrid.Children.Add(cell);
                }
            }

            //creating the highlight and setting it's length
            _highlight = new Rectangle();
            _highlight.Fill = new SolidColorBrush(Color.FromArgb(255 / 2, 255, 255, 0));
            _highlight.Visibility = Visibility.Hidden;
            resultsGrid.Children.Add(_highlight);
            Grid.SetRowSpan(_highlight, numRows);
            Grid.SetZIndex(_highlight, 2); // must be between 0 and 5

            curTestPoint = TagCollection.Get("CurrentTestPoint");
            curTestPoint.ValueSet += curTestPoint_ValueSet;
            curTestPoint.ValueChanged += curTestPoint_ValueSet;

            //keeping the result values updated
            foreach (DataTag result in _resultValueTags)
            {
                result.ValueChanged += result_ValueChanged;
                result.ValueSet += result_ValueChanged;
            }

            foreach (DataTag sigma in _resultSigmaTags)
            {
                sigma.ValueChanged += result_ValueChanged;
                sigma.ValueSet += result_ValueChanged;
            }
        }

        void curTestPoint_ValueSet(object sender, EventArgs e)
        {
            if (curTestPoint.Int == 0 || curTestPoint.Int >= numCols)
                _highlight.Visibility = Visibility.Hidden;
            else
            {
                _highlight.Visibility = Visibility.Visible;
                Grid.SetColumn(_highlight, curTestPoint.Int);
            }



        }

        void result_ValueChanged(object sender, EventArgs e)
        {
            DataTag tag = (sender as DataTag);
            ColumnDefinition colDef = null;
            RowDefinition rowDef = null; 

            if (tag.Double == tag.LastDouble) return; //if the value hasn't changed, return

            //extracting the row and col from the name and description of the tag
            //name format is 'TestPoint1', here I'm getting the '1'
            try
            {
                colDef = resultsGrid.ColumnDefinitions.Single((c) => c.Name == "Header" + Regex.Match(tag.Name, @"[\d+]").Value);
                rowDef = resultsGrid.RowDefinitions.Single((r) => r.Name == tag.Name.Split('.').Last());
            }
            catch { return; }

            //getting the index of the row and col
            int col = resultsGrid.ColumnDefinitions.IndexOf(colDef);
            int row = resultsGrid.RowDefinitions.IndexOf(rowDef);

            //getting the result cell from the index
            ResultCell cell = _cells[row][col];

            if (tag.Name.ToLower().Contains("sigma"))
            {
                //if this is a sigma value, make the sigma part of the cell visible and shrink the result part
                cell.sigmaBorder.Visibility = System.Windows.Visibility.Visible;
                cell.SigmaValue.Text = (double.IsNaN(tag.Double) ? String.Empty : tag.Text);
                Grid.SetColumnSpan(cell.resultBorder, 1);
                cell.gridCell.ColumnDefinitions[1].Width = new GridLength(4, GridUnitType.Star);

                _setBackground(tag, cell, true);
            }
            else
            {
                cell.ResultValue.Text = (double.IsNaN(tag.Double) ? String.Empty : tag.Text);

                _setBackground(tag, cell);
            }


        }

        private static void _setBackground(DataTag tag, ResultCell cell, bool sigma = false)
        {
            if (double.IsNaN(tag.Double))
                cell.SetWhiteBackground(sigma);
            else if (double.IsNaN(tag.Max) && double.IsNaN(tag.Min))
                cell.SetWhiteBackground(zIndex: 5);
            else if (tag.InRange)
                cell.SetGreenBackground(sigma);
            else
                cell.SetRedBackground(sigma);
        }

        private void ClearResults_Click(object sender, RoutedEventArgs e)
        {
            Tecmosa.Sequences.Results.clearResults();
        }

        private void resultsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }
    }
}
