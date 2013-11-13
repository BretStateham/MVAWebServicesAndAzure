using System;
using System.Collections.Generic;
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

namespace EFDemos.CodeFirst
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void AddPosition_Click(object sender, RoutedEventArgs e)
    {
      using (PositionsContext ctx = new PositionsContext())
      {
        double latitude = double.TryParse(LatitudeText.Text, out latitude) ? latitude : 0;
        double longitude = double.TryParse(LongitudeText.Text, out longitude) ? longitude : 0;
        Position position = new Position
        {
          Latitude = latitude,
          Longitude = longitude
        };
        ctx.Positions.Add(position);
        ctx.SaveChanges();

        PositionsList.ItemsSource = ctx.Positions.ToList();

      }
    }
  }
}
