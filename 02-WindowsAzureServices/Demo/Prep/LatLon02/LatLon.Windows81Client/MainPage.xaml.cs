using Bing.Maps;
using LatLon.Windows81Client.LatLonWcf;
using LatLon.Windows81Client.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
  public sealed partial class MainPage : Page, INotifyPropertyChanged
  {
    #region INotifyPropertyChanged Implementation
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected void Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
    {
      if (!EqualityComparer<T>.Default.Equals(field, value))
      {
        field = value;
        NotifyPropertyChanged(PropertyName);
      }
    }

    protected void NotifyPropertyChanged(string PropertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
    }
    
    #endregion INotifyPropertyChanged Implementation

    Location tapLocation;
    
    private double startLatitude = 0.0d;

    public double StartLatitude
    {
      get { return startLatitude; }
      set { Set(ref startLatitude,value); }
    }

    private double startLongitude = 0.0d;

    public double StartLongitude
    {
      get { return startLongitude; }
      set { Set(ref startLongitude, value); }
    }

    private double endLatitude = 0.0d;

    public double EndLatitude
    {
      get { return endLatitude; }
      set { Set(ref endLatitude, value); }
    }

    private double endLongitude = 0.0d;

    public double EndLongitude
    {
      get { return endLongitude; }
      set { Set(ref endLongitude, value); }
    }
    


    public MainPage()
    {
      this.InitializeComponent();

      this.DataContext = this;

      //InitializeMapControl();

    }

    //private void InitializeMapControl()
    //{
    //  Map = new Map();
    //  Map.Credentials = "AtWb8v72G_Qi2C-_TKGuHl8yGwTLU_C7mTM5NRgruXC6amw6QUox1Cpv-A1Az8eh";
    //  Map.RightTapped += map_RightTapped;
    //  Map.Margin = new Thickness(10d);
    //  Grid.SetRow(Map, 4);
    //  Grid.SetColumn(Map, 1);
    //  LayoutRoot.Children.Add(Map);

    //  MapLayer pushpins = this.Resources["Pushpins"] as MapLayer;
    //  if (pushpins != null)
    //    Map.Children.Add(pushpins);

    //  //pushpins = new MapLayer();
    //  //map.Children.Add(pushpins);

    //  //startPin = new Pushpin()
    //  //{
    //  //  Background = new SolidColorBrush(Colors.Green)
    //  //};

    //  //endPin = new Pushpin()
    //  //{
    //  //  Background = new SolidColorBrush(Colors.Red)
    //  //};

    //  //MapLayer.SetPosition(startPin, new Location(0, 0));
    //  //MapLayer.SetPosition(endPin, new Location(0, 0));

    //  //pushpins.Children.Add(startPin);
    //  //pushpins.Children.Add(endPin);


    //  //Get rid of the place holder control at runtime.
    //  MapPlaceHolder = null;
    //}

    void Map_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
      Point pagePosition = e.GetPosition(this);
      Point mapPosition = e.GetPosition(Map);

      bool succeeded = Map.TryPixelToLocation(mapPosition, out tapLocation);
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
            //StartLatText.Text = tapLocation.Latitude.ToString();
            //StartLonText.Text = tapLocation.Longitude.ToString();
            //MapLayer.SetPosition(StartPin, tapLocation);
            StartLatitude = tapLocation.Latitude;
            StartLongitude = tapLocation.Longitude;
            
            break;
          case "setend":
            //EndLatText.Text = tapLocation.Latitude.ToString();
            //EndLonText.Text = tapLocation.Longitude.ToString();
            //MapLayer.SetPosition(EndPin, tapLocation);
            EndLatitude = tapLocation.Latitude;
            EndLongitude = tapLocation.Longitude;
            break;
        }
        SetMapView();
      }
      if (SetTargetPopup.IsOpen) SetTargetPopup.IsOpen = false;
    }

    private void SetMapView()
    {
      LocationCollection locations = new LocationCollection{
        new Location(StartLatitude,StartLongitude),
        new Location(EndLatitude,EndLongitude)
      };
      LocationRect rect = new LocationRect(locations);
      rect.Width += 2;
      rect.Height += 2;
      Map.SetView(rect);
      //Map.SetZoomLevel(Map.ZoomLevel - 1);
    }


  }
}
