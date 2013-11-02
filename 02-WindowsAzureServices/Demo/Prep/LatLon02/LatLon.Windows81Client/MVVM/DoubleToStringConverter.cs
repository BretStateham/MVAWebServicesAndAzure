using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace LatLon.Windows81Client.MVVM
{
  public class DoubleToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      Double val = (double)value;
      return val.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      double val = 0d;
      double.TryParse(value.ToString(), out val);
      return val;
    }
  }
}
