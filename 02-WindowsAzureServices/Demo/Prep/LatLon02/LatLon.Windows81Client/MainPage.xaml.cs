using Bing.Maps;
using LatLon.Windows81Client.LatLonWcf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LatLon.Windows81Client
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page
  {
    Map map;
    MapLayer pushpins;
    Pushpin startPin;
    Pushpin endPin;

    Location tapLocation;

    public MainPage()
    {
      this.InitializeComponent();

      InitializeMapControl();

    }

    private void InitializeMapControl()
    {
      map = new Map();
      map.Credentials = "AtWb8v72G_Qi2C-_TKGuHl8yGwTLU_C7mTM5NRgruXC6amw6QUox1Cpv-A1Az8eh";
      map.RightTapped += map_RightTapped;
      map.Margin = new Thickness(10d);
      Grid.SetRow(map, 4);
      Grid.SetColumn(map, 1);
      LayoutRoot.Children.Add(map);


      pushpins = new MapLayer();
      map.Children.Add(pushpins);

      startPin = new Pushpin()
      {
        Background = new SolidColorBrush(Colors.Green)
      };

      endPin = new Pushpin()
      {
        Background = new SolidColorBrush(Colors.Red)
      };

      MapLayer.SetPosition(startPin, new Location(0, 0));
      MapLayer.SetPosition(endPin, new Location(0, 0));

      pushpins.Children.Add(startPin);
      pushpins.Children.Add(endPin);


      //Get rid of the place holder control at runtime.
      MapPlaceHolder = null;
    }

    void map_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
      Point pagePosition = e.GetPosition(this);
      Point mapPosition = e.GetPosition(map);

      bool succeeded = map.TryPixelToLocation(mapPosition, out tapLocation);
      if (succeeded)
      {

        SetTargetPopup.HorizontalOffset = Math.Max(Math.Min(pagePosition.X + 10, this.ActualWidth - SetTargetPopupBorder.Width), 0);
        SetTargetPopup.VerticalOffset = Math.Max(Math.Min(pagePosition.Y + 10, this.ActualHeight - SetTargetPopupBorder.Height), 0);

        if (!SetTargetPopup.IsOpen) { SetTargetPopup.IsOpen = true; }

        //Pushpin pin = new Pushpin();
        //mapLayer.Children.Add(pin);
        //MapLayer.SetPosition(pin, location);
      }
    }

    private async void GetDistanceButton_Click(object sender, RoutedEventArgs e)
    {
      double latStart;
      double lonStart;
      double latEnd;
      double lonEnd;
      string message = string.Empty;

      if (double.TryParse(StartLatText.Text, out latStart))
        if (double.TryParse(StartLonText.Text, out lonStart))
          if (double.TryParse(EndLatText.Text, out latEnd))
            if (double.TryParse(EndLonText.Text, out lonEnd))
            {

              LoadingRing.IsActive = true;

              try
              {
                LatLonUtilitiesServiceClient svc = new LatLonUtilitiesServiceClient();

                DistanceList.Items.Clear();
                DistanceList.Items.Add(string.Format("Distance (Rads): {0}", await svc.RadiansBetweenToPointsAsync(latStart, lonStart, latEnd, lonEnd)));
                DistanceList.Items.Add(string.Format("Distance (NM):   {0}", await svc.NauticalMilesBetweenToPointsAsync(latStart, lonStart, latEnd, lonEnd)));
                DistanceList.Items.Add(string.Format("Distance (KM):   {0}", await svc.KilometersBetweenToPointsAsync(latStart, lonStart, latEnd, lonEnd)));
                DistanceList.Items.Add(string.Format("Distance (MI):   {0}", await svc.MilesBetweenToPointsAsync(latStart, lonStart, latEnd, lonEnd)));

              }
              catch (Exception ex)
              {
                message = string.Format("An error occurred while retrieving distances from the service: {0} - {1}",ex.GetType().Name,ex.Message);
              }

              LoadingRing.IsActive = false;

              if(!string.IsNullOrWhiteSpace(message))
              {
                MessageDialog dlg = new MessageDialog(message);
                await dlg.ShowAsync();
              }

            }
    }

    private void SetTargetPosition_Click(object sender, RoutedEventArgs e)
    {
      Button button = sender as Button;
      if(button != null)
      {
        string tag = button.Tag.ToString().ToLower();
        switch(tag)
        {
          case "setstart":
            StartLatText.Text = tapLocation.Latitude.ToString();
            StartLonText.Text = tapLocation.Longitude.ToString();
            MapLayer.SetPosition(startPin, tapLocation);
            break;
          case "setend":
            EndLatText.Text = tapLocation.Latitude.ToString();
            EndLonText.Text = tapLocation.Longitude.ToString();
            MapLayer.SetPosition(endPin, tapLocation);
            break;
        }
      }
      if (SetTargetPopup.IsOpen) SetTargetPopup.IsOpen = false;
    }

  }
}
