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

namespace EFDemos.DesignFirst
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

    private void GetPositionsButton_Click(object sender, RoutedEventArgs e)
    {
      PositionsContext ctx = new PositionsContext();

      var positions = (from p in ctx.Positions
                       where p.CruiseID == 1
                       orderby p.ReportedAt
                       select p).Take(5);

      PositionsList.ItemsSource = positions.ToList();
    }
  }
}
