using EFDemos.WcfDataClient.PositionsService;
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

namespace EFDemos.WcfDataClient
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
      PositionsContext ctx = new PositionsContext(new Uri("http://localhost:7625/PositionsService.svc"));

      var positions = (from p in ctx.Positions
                       where p.CruiseID == 1
                       orderby p.ReportedAt descending
                       select p).Take(5);

      PositionsList.ItemsSource = positions.ToList();

    }
  }
}
