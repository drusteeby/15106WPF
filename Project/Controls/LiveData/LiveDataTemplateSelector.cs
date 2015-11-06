using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Tecmosa.Controls.LiveData
{
    /*
                    <ItemsControl.ItemTemplate>
                    <DataTemplate >
                        <Border Margin="2" Background="LightBlue">
                            <Grid>
                                <Grid.ColumnDefinitions>
                                    <ColumnDefinition SharedSizeGroup="TagName" Width="Auto"/>
                                    <ColumnDefinition SharedSizeGroup="TagData" Width="100"/>
                                </Grid.ColumnDefinitions>
                                <Border  Grid.Column="0" BorderBrush="Black" BorderThickness="2,2,1,2">
                                    <TextBlock Margin="5"    Text="{Binding Path=Name}"/>
                                </Border>
                                <Border  DockPanel.Dock="Top"  Grid.Column="1" BorderBrush="Black"  BorderThickness="1,2,2,2">
                                        <TextBlock  Text="{Binding Path=Value}"
                                                    Margin="5"
                                                   HorizontalAlignment="Center"
                                                   VerticalAlignment="Center"/>

                                </Border>
                            </Grid>
                        </Border>
                    </DataTemplate>
                </ItemsControl.ItemTemplate>
                */

    public static class GridWidths
    {
        public static int Row1Width;
        public static int Row2Width;
    }

    public class LiveDataTemplateSelector: DataTemplateSelector
    {

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return base.SelectTemplate(item, container);
        }
    }
}
