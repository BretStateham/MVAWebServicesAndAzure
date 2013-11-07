using Bing.Maps;
using LatLon.Clients.Win81Client.LatLonWcf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LatLon.Clients.Win81Client
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

    /*
     * Start:   47.620553946572,  -122.349371687757 //One Microsoft Way
     * End:     47.6396336616716, -122.128283751696 //Space Needle
     */

    private double startLatitude = 47.620553946572d;

    public double StartLatitude
    {
      get { return startLatitude; }
      set
      {
        Set(ref startLatitude, value);
        SetMapView();
      }
    }

    private double startLongitude = -122.349371687757d;

    public double StartLongitude
    {
      get { return startLongitude; }
      set
      {
        Set(ref startLongitude, value);
        SetMapView();
      }
    }

    private double endLatitude = 47.6396336616716d;

    public double EndLatitude
    {
      get { return endLatitude; }
      set
      {
        Set(ref endLatitude, value);
        SetMapView();
      }
    }

    private double endLongitude = -122.128283751696d;

    public double EndLongitude
    {
      get { return endLongitude; }
      set
      {
        Set(ref endLongitude, value);
        SetMapView();
      }
    }

    public MainPage()
    {
      this.InitializeComponent();

      this.DataContext = this;

      this.Loaded += MainPage_Loaded;

    }

    void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
      //Set the map view to include the starting and ending points.
      SetMapView();
    }

    /// <summary>
    /// Map RightTapped event handler.  Allows the user to set the start or end position based on the location of the map they right-tapped on. 
    /// </summary>
    /// <param name="sender">The map control that was tapped</param>
    /// <param name="e">The RightTappedRoutedEventArgs including the position of the tap</param>
    void Map_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
      Point pagePosition = e.GetPosition(this);
      Point mapPosition = e.GetPosition(Map);

      //Turn the x/y position that was tapped on the map to a lat/lon location...
      bool succeeded = Map.TryPixelToLocation(mapPosition, out tapLocation);
      //if that worked
      if (succeeded)
      {
        //Make sure the pop-up menu will be on the screen
        SetTargetPopup.HorizontalOffset = Math.Max(Math.Min(pagePosition.X + 10, this.ActualWidth - SetTargetPopupBorder.Width), 0);
        SetTargetPopup.VerticalOffset = Math.Max(Math.Min(pagePosition.Y + 10, this.ActualHeight - SetTargetPopupBorder.Height), 0);
        //and show it.  
        if (!SetTargetPopup.IsOpen) { SetTargetPopup.IsOpen = true; }
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
              //Show the progress ring...
              LoadingRing.IsActive = true;

              try
              {
                //Use the LatLonWcf Service Reference to create a LatLonUtiltiesSerivce client proxy...
                LatLonUtilitiesServiceClient svc = new LatLonUtilitiesServiceClient();

                //Then call the various methods on the service to determine the distance 
                //Between the start and end points. 
                DistanceList.Items.Clear();
                DistanceList.Items.Add(string.Format("Retrieving Distances From: {0}", svc.Endpoint.Address.Uri.ToString()));
                DistanceList.Items.Add(string.Format("Distance (Rads): {0}", await svc.RadiansBetweenTwoPointsAsync(latStart, lonStart, latEnd, lonEnd)));
                DistanceList.Items.Add(string.Format("Distance (NM):   {0}", await svc.NauticalMilesBetweenTwoPointsAsync(latStart, lonStart, latEnd, lonEnd)));
                DistanceList.Items.Add(string.Format("Distance (KM):   {0}", await svc.KilometersBetweenTwoPointsAsync(latStart, lonStart, latEnd, lonEnd)));
                DistanceList.Items.Add(string.Format("Distance (MI):   {0}", await svc.MilesBetweenTwoPointsAsync(latStart, lonStart, latEnd, lonEnd)));

              }
              catch (Exception ex)
              {
                //Build an error message
                message = string.Format("An error occurred while retrieving distances from the service: {0} - {1}", ex.GetType().Name, ex.Message);
              }

              //Hide the progress ring
              LoadingRing.IsActive = false;

              //If there is an error message, show it in a MessageDialog
              if (!string.IsNullOrWhiteSpace(message))
              {
                MessageDialog dlg = new MessageDialog(message);
                await dlg.ShowAsync();
              }
            }
    }

    /// <summary>
    /// Set the start or end position based on which PopUp button was clicked...
    /// </summary>
    /// <param name="sender">The button that was clicked</param>
    /// <param name="e">The RoutedEventArgs for the click event</param>
    private void SetTargetPosition_Click(object sender, RoutedEventArgs e)
    {
      Button button = sender as Button;
      if (button != null)
      {
        string tag = button.Tag.ToString().ToLower();
        switch (tag)
        {
          //If the setstart button was clicked, set the start position...
          case "setstart":
            StartLatitude = tapLocation.Latitude;
            StartLongitude = tapLocation.Longitude;
            break;
          //if the setend button was clicked, set the end position...
          case "setend":
            EndLatitude = tapLocation.Latitude;
            EndLongitude = tapLocation.Longitude;
            break;
        }
      }
      if (SetTargetPopup.IsOpen) SetTargetPopup.IsOpen = false;
    }

    private void SetMapView()
    {
      //Build a location collection that includes the start and end positions
      LocationCollection locations = new LocationCollection{
        new Location(StartLatitude,StartLongitude),
        new Location(EndLatitude,EndLongitude)
      };
      //Build a LocationRectangle using that surrounds all locations in the collection
      LocationRect rect = new LocationRect(locations);

      //Increase the size of the rectangle to make sure no locations are on the border of the rectangle
      rect.Width *= 1.75;
      rect.Height *= 1.75;

      //Set the map view to zoom in on the LocationRect...
      Map.SetView(rect);
    }
  }
}
