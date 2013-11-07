<a name="Title" />
# Web Services in Windows Azure Demo Snippets #
---

##FOR ALL DEMOS, MAKE SURE YOU ARE RUNNING ON WINDOWS 8.1, AND VISUAL STUDIO 2013 WITH THE WINDOWS AZURE SDK 2.2 OR LATER.  YOU MUST ALSO RUN VISUAL STUDIO 2013 AS ADMINISTRATOR##

#### Review the demo project: ####

 - [Review LatLon.Core.Contracts](#ReviewContracts)
 - [Review LatLon.Core.Services](#ReviewServices)
 - [Review LatLon.Hosts.Console](#ReviewConsoleHost)
 - [Review the Windows 8.1 Client](#ReviewWin81Client)
 - [Run the Console Host](#RunTheConsoleHost)
 - [Update Win 8.1 Client Service Reference to Console Host](#UpdateServiceReferenceConsoleHost)

#### Host the Service in an Azure Web Site: ####

 - [Add a Web Host Project](#AddWebHostProject)
 - [Create a Web Site in the Azure Management Portal](#CreateSiteInPortal)
 - [Publish to a Windows Azure Web Site](#PublishWindowsAzureWebSite)

#### Host the Service in an Azure Web Role: ####

 - [Create the Cloud Service and Web Role Projects](#CreateCloudServiceAndWebRole)
 - [Add a WCF Service to the Web Role](#AddWcfServceToWebRole)
 - [Publish the Cloud Service to Windows Azure](#PublishTheCloudService)

#### Host the Service in an Azure Worker Role: ####

 - [Add a Worker Role to the Cloud Service](#AddAWorkerRole)
 - [Implement the Worker Role Host](#ImplementWorkerRolehost)
 - [Run the Worker Role in the Emulator](#RunWorkerRoleInEmulator)
 - [Re-Publish the Cloud Service to Azure](#RePublishCloudService)

---











## Review the Demo Project ##










<!-- ======================================================================= -->
<a name="ReviewContracts" />
### Review LatLon.Core.Contracts ###
<!-- ======================================================================= -->
---

Open the initial demo solution, and review the contents of the LatLon.Core.Contracts project

 - Open the **/Demos/Begin/01LatLonBaseProject/LatLon.sln** solution

 - Expand the LatLon.Core.Contracts project

 - Expand **"References"** and note the **System.ServiceModel** reference

 - Open the **"ILatLonUtilitiesService.cs"** file, and review the code for the **ILatLonUtilitiesService** interface

 - Note the following:
    - The **[ServiceContract]** attribute
    - The four methods 
      - **RadiansBetweenTwoPoints**, 
      - **NauticalMilesBetweenTwoPoints**, 
      - **KilometersBetweenTwoPoints**, 
      - **MilesBetweenTwoPoints**
    - Each method is decorated with the **[OperationContract]** attribute

````C#
[ServiceContract(Namespace = "http://BretStateham.com/samples/2013/10/LatLon")]
public interface ILatLonUtilitiesService
{

  [OperationContract]
  double RadiansBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

  [OperationContract]
  double NauticalMilesBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

  [OperationContract]
  double KilometersBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

  [OperationContract]
  double MilesBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

}
````

<!-- ======================================================================= -->
<a name="ReviewServices" />
### Review LatLon.Core.Services ###
<!-- ======================================================================= -->
---

Show the default implementation of the LatLonUtilitiesService

 - In the **Solution Explorer**,expand the **LatLon.Core.Services** project.
 - Expand the **References**,  and note the **LatLon.Core.Contracts** reference.
 - Open the **LatLonUtilitiesService.cs** code file

 - Review the code for each of the **&lt;Units&gt;BetweenTwoPoints** methods

 - The code assumes an elliptical, not flat, world and calculates the distances between to points, where each point has a latitude and a longitude.  It can then return that distances as Radians, Nautical Miles, Kilometers, or Miles.  

 - Web references:
   - Haversine Forumula: http://en.wikipedia.org/wiki/Haversine_formula 
   - Reference Implementation: http://www.movable-type.co.uk/scripts/latlong.html

````C#
public class LatLonUtilitiesService : ILatLonUtilitiesService
{
  public double RadiansBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
  {
    //Using Haversine Forumla: http://en.wikipedia.org/wiki/Haversine_formula 
    //Reference Implementation: http://www.movable-type.co.uk/scripts/latlong.html

    double deg2rad = (Math.PI / 180.0);

    double dLat = (Latitude2 - Latitude1) * deg2rad;
    double dLon = (Longitude2 - Longitude1) * deg2rad;
    double lat1 = Latitude1 * deg2rad;
    double lat2 = Latitude2 * deg2rad;

    double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Pow(Math.Sin(dLon / 2), 2) * Math.Cos(lat1) * Math.Cos(lat2);

    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

    return c;
  }

  public double NauticalMilesBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
  {
    //Convert to Nautical miles by first converting back to degrees, then multiplying by 60NM (Nautical Miles) / Degree
    //distance = (distance * 180 / PI) * 60;
    return RadiansBetweenTwoPoints(Latitude1, Longitude1, Latitude2, Longitude2) * (180 / Math.PI) * 60;
  }

  public double KilometersBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
  {
    //Convert to Kilometers by multiplying the distance in radians by the earth's radius in kilometers, 6371 (see http://en.wikipedia.org/wiki/Earth_radius) 
    //distance *= 6371;
    return RadiansBetweenTwoPoints(Latitude1, Longitude1, Latitude2, Longitude2) * 6371;
  }

  public double MilesBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
  {
    //Convert to Miles by multiplying the distance in radians by the earth's radius in miles, 3959 (see http://en.wikipedia.org/wiki/Earth_radius) 
    //distance *= 3959;
    return RadiansBetweenTwoPoints(Latitude1, Longitude1, Latitude2, Longitude2) * 3959;
  }
}
````

<!-- ======================================================================= -->
<a name="ReviewConsoleHost" />
### Review LatLon.Hosts.Console ###
<!-- ======================================================================= -->
---

Review the Console Host implementation.  Remember that most any manageded program can be a service host.  This could be an ASP.NET web application, a Windows Service, a Windows Forms or WPF Desktop app, or even a console application, as this sample project demonstrates.

 - In the **Solution Explorer**, expand the **LatLon.Hosts.Console** project
 - Expand **References** and note the following references: 
    - **LatLon.Core.Contracts**
    - **LatLon.Core.Services**
    - **System.ServiceModel**

 - Open the Program.cs file and review the code inside.  The comments in the code should explain it's function:

````C#
class Program
{
  static void Main(string[] args)
  {

    //Create a new ServiceHost intance with a "using" statement.  
    //The using statement makes sure that the host is "Dispose"d even if there
    //is an exception. 
    //The host will offer the LatLonUtilitiesService methods up to it's clients
    //and listen on the http://localhost:8000/LatLon URL for service call as well 
    //as metadata requests. This base address will be the root URL for all endpoints.
    using (ServiceHost host = 
            new ServiceHost(
              typeof(LatLonUtilitiesService), 
              new Uri("http://localhost:8000/LatLon")))
    {

      //A single service can be exposed through any number of "Endpoints". 
      //An endpoint has an Address, a Binding, and a Contract that it supports.  
      //For this example, we'll add a single endpoint that has the following 
      //Contract: And ILatLonUtilitiesService implementation
      //Binding:  BasicHttpBinding - using basic http calls (no, WS*, WSE, Security, etc)
      //Address:  http://localhost:8000/LatLon/LatLonUtilities (host base address + endpoint address)
      host.AddServiceEndpoint(
        typeof(ILatLonUtilitiesService),
        new BasicHttpBinding(),
        "LatLonUtilities");

      //Allow our service to expose metadata to clients.  This metadata 
      //allows client applications on any platform to build their own 
      //proxy classes without having to have access to the original contracts. 
      ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
      smb.HttpGetEnabled = true;
      smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
      host.Description.Behaviors.Add(smb);

        
      //Listen for metadata requests using an endpoint with:
      //Contract:  ServiceMetadataBehavior.MexContractName (IMetadataExchange)
      //Binding:   MetadataExchangeBindings.CreateMexHttpBinding() (A WSHttpBinding with no security)
      //Address:   http://localhost:8000/LatLon/mex (host base address + endpoint address)
      host.AddServiceEndpoint(
        ServiceMetadataBehavior.MexContractName,
        MetadataExchangeBindings.CreateMexHttpBinding(),
        "mex");
        
      //Start the service, and begin listening for requests
      host.Open();

      //Show the addresses that the host is listening on...
      StringBuilder sb = new StringBuilder(
        "======================================================================\n" + 
        "Listening On:\n" +
        "======================================================================\n\n");
      foreach(var cd in host.ChannelDispatchers)
      {
        sb.AppendFormat("  {0} ({1})\n", cd.Listener.Uri.ToString(),cd.State.ToString());
      }
      sb.Append(
        "\n======================================================================");
      Console.WriteLine(sb.ToString());

      //Keep the service running until the user presses the <Enter> key
      Console.WriteLine("Service Started...");
      Console.WriteLine("Press <ENTER> to terminate:");
      Console.ReadLine();

      //Stop the service
      Console.WriteLine("Shutting Down...");
      host.Close();
      Console.WriteLine("Done...");

    }
  }
}
````

<!-- ======================================================================= -->
<a name="ReviewWin81Client" />
### Review the Windows 8.1 Client ###
<!-- ======================================================================= -->
---

The last project we'll review right now is a Windows 8.1 Client app to calls into the LotLonUtilitiesService to retrieve the distances between two points on a map.  

 - In the **Solution Explorer**, expand **LatLon.Clients.Win81Client**.
 - Expand **"Service References"** and make not of the **LatLonWcf** service reference.   
 - The Windows 8.1 Client uses the Bing Maps SDK to display a map that the user can use to select starting lat/lon and ending lat/lon.  There are sometimes display issues that prevent the Bing Maps control from displaying.  
 - Open the MainPage.xaml document and review the markup.  Key elements include:
    - TextBox controls for the StartLatText, StartLonText, EndLatText, EndLonText
    - GetDistanceButton Button control to get the distances between the two points
    - Map control to display the start and end locations
    - Popup control to allow the user to right click the map to set start or end locations.

````XML
<Page
    x:Class="LatLon.Clients.Win81Client.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:LatLon.Clients.Win81Client"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:cnv="using:LatLon.Clients.Win81Client.Converters"
    xmlns:maps="using:Bing.Maps"
    mc:Ignorable="d">

  <Page.Resources>
    <cnv:DoubleToStringConverter x:Key="DoubleToStringConverter" />
  </Page.Resources>

  <Grid x:Name="LayoutRoot" Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

    <Grid.ColumnDefinitions>
      <ColumnDefinition/>
      <ColumnDefinition Width="2*"/>
      <ColumnDefinition Width="2*"/>
      <ColumnDefinition/>
    </Grid.ColumnDefinitions>
    <Grid.RowDefinitions>
      <RowDefinition Height="16*"/>
      <RowDefinition Height="91*"/>
      <RowDefinition Height="103*"/>
      <RowDefinition Height="90*"/>
      <RowDefinition Height="468*"/>
    </Grid.RowDefinitions>

    <TextBlock Grid.Row="1" Grid.Column="1" Text="Latitude" Style="{StaticResource HeaderTextBlockStyle}" HorizontalAlignment="Center" VerticalAlignment="Center" />
    <TextBlock Grid.Row="1" Grid.Column="2" Text="Longitude" Style="{StaticResource HeaderTextBlockStyle}" HorizontalAlignment="Center" VerticalAlignment="Center" />

    <TextBlock Grid.Row="2" Text="Start" Style="{StaticResource HeaderTextBlockStyle}" HorizontalAlignment="Left" VerticalAlignment="Center" Margin="10" />
    <TextBlock Grid.Row="3" Text="End" Style="{StaticResource HeaderTextBlockStyle}" HorizontalAlignment="Left" VerticalAlignment="Center" Margin="10" />

    <TextBox x:Name="StartLatText" Grid.Row="2" Grid.Column="1" Margin="10" Text="{Binding StartLatitude, Converter={StaticResource DoubleToStringConverter}, Mode=TwoWay}" FontSize="48" />
    <TextBox x:Name="StartLonText" Grid.Row="2" Grid.Column="2" Margin="10" Text="{Binding StartLongitude, Converter={StaticResource DoubleToStringConverter}, Mode=TwoWay}" FontSize="48" />

    <TextBox x:Name="EndLatText" Grid.Row="3" Grid.Column="1" Margin="10" Text="{Binding EndLatitude, Converter={StaticResource DoubleToStringConverter}, Mode=TwoWay}" FontSize="48" />
    <TextBox x:Name="EndLonText" Grid.Row="3" Grid.Column="2" Margin="10" Text="{Binding EndLongitude, Converter={StaticResource DoubleToStringConverter}, Mode=TwoWay}" FontSize="48" />

    <Button x:Name="GetDistanceButton" Grid.Row="2" Grid.Column="3" Content="Get Distances" Grid.RowSpan="2" Margin="10" VerticalAlignment="Stretch" Width="200" Click="GetDistanceButton_Click" />

    <TextBlock Grid.Row="4" TextWrapping="Wrap" Text="Right click map to set start or end position:" VerticalAlignment="Center" HorizontalAlignment="Left" FontSize="24" Margin="10"/>


    <maps:Map x:Name="Map" Grid.Row="4" Grid.Column="1" Margin="10" Credentials="{StaticResource BingMapsCredentials}" RightTapped="Map_RightTapped">
      <maps:MapLayer x:Name="Pushpins">
        <maps:Pushpin x:Name="StartPin" Background="Green">
          <maps:MapLayer.Position>
            <maps:Location Latitude="{Binding StartLatitude}" Longitude="{Binding StartLongitude}" />
          </maps:MapLayer.Position>
        </maps:Pushpin>
        <maps:Pushpin x:Name="EndPin" Background="Red">
          <maps:MapLayer.Position>
            <maps:Location Latitude="{Binding EndLatitude}" Longitude="{Binding EndLongitude}" />
          </maps:MapLayer.Position>
        </maps:Pushpin>
      </maps:MapLayer>
    </maps:Map>

    <Popup x:Name="SetTargetPopup" HorizontalOffset="200" VerticalOffset="10" IsLightDismissEnabled="True">
      <Popup.ChildTransitions>
        <TransitionCollection>
          <PopupThemeTransition />
        </TransitionCollection>
      </Popup.ChildTransitions>
      <Border x:Name="SetTargetPopupBorder" BorderBrush="{StaticResource ApplicationForegroundThemeBrush}" BorderThickness="2" Background="{StaticResource ApplicationPageBackgroundThemeBrush}" Width="200" Height="200">
        <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
          <Button x:Name="SetStartPosition" Content="Set Start Position"  HorizontalAlignment="Center" Tag="SetStart" Click="SetTargetPosition_Click" Width="175" Margin="10" />
          <Button x:Name="SetEndPosition" Content="Set End Position"  HorizontalAlignment="Center" Tag="SetEnd" Click="SetTargetPosition_Click" Width="175" Margin="10"/>
        </StackPanel>
      </Border>
    </Popup>

    <ListView x:Name="DistanceList" Grid.Row="4" Grid.Column="2" Margin="10" />

    <ProgressRing x:Name="LoadingRing" Grid.Row="4" Grid.Column="2" Margin="10" Width="200" Height="200" />

  </Grid>
</Page>
````
 - Open the MainPage.xaml.cs code file to review the code.  The most important method is the **GetDistanceButton_Click** event handler.  This is where the call to the Wcf service is made, and the results displayed:

````C#
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
````

<!-- ======================================================================= -->
<a name="RunTheConsoleHost" />
### Run the Console Host ###
<!-- ======================================================================= -->
---

Run the Console Host (without debugging).  

 - Right-click the **LatLon** Solution in the **Solution Explorer** window, and select **"Properties"**
 - Switch to the **"Common Properties"** | **"Startup Project"** page, and ensure that **"Current selection"** is selected, then click **OK**.  This tells Visual Studio to run what ever project is current selected.  

    ![Debug Current Selection](images/01debug-current-selection.png?raw=true)

 - In the **Solution Explorer** window, select the **LatLon.Hosts.ConsoleHost** project.  
 - From the menu bar select **"Debug"** | **"Start Without Debugging"** or press **Ctrl+F5**. A console window should appear and display the status of the service host: 

    ![Console Hosted Service](images/02console-hosted-service.png?raw=true)

 - Note the URLs that are being listened on by the serivce, and copy the last one (should be **http://locahost:8000/LatLon**) to the clipboard.

 - Leave the console app running and return to **Visual Studio**

````C#
 //here is some code
````

<!-- ======================================================================= -->
<a name="UpdateServiceReferenceConsoleHost" />
### Update Win 8.1 Client Service Reference to Console Host ###
<!-- ======================================================================= -->
---

Ensure that the Windows 8.1 Client application's LatLonWcf service reference is pointing at the correct URL for the ClientHost service endpoint

 - In the **"Solution Explorer"** window, expand **"LatLon.Clients.Win81Client"** | **"Service References"**
 - Right-click the **LatLonWcf** service reference and select **"Configure Service Reference..."**
 - In the **"Service Referece Settings"** window, in the **"Address"** field, paste (or type) in the endpoint URL copied from the ConsoleHost console window previously (should be **http://localhost:8000/LatLon** unless you changed the code).  Click **"OK"** to update the service reference. 

    ![Configure Service Reference](images/03configure-service-reference.png?raw=true) 

 - In the **Solution Explorer**, select the **"LatLon.Clients.Win81Client"** project.
 - From the menu bar select **"Debug"** | **"Start Without Debugging"** or press **Ctrl+F5**.
 - The Windows 8.1 Client application should start.  Click the "Get Distances" button to verify that the service call functioned correctly. 

    > **Note:** If there are build errors, Right click the "LatLon" solution and select "Clean Solution", then right-click the solution and select "Rebuild Solution". If needed, stop the console host before rebuilding and re-start it afterwards.  

	![Win81ClientRunning](images/04win81clientrunning.png?raw=true)

````C#
 //here is some code
````











## Host the Service in an Azure Web Site ##











<!-- ======================================================================= -->
<a name="AddWebHostProject" />
### Add a Web Host Project ###
<!-- ======================================================================= -->
---

Add a web project to act as a service host.  

 - In the Visual Studio **Solution Explorer**, right click the **"LatLon"** solution and select **"Add"** | **"New Project..."**
 - In the **"Add New Project"** window, from the **"Installed"** templates, select **"Visual C#"** | **"Web"** | **"ASP.NET Web Application"**.  Name the project **LatLon.Hosts.WebHost** and click **"OK"** to add the new project. 

	![Add Web Host Project](images/05add-web-host-project.png?raw=true)

 - In the **"New ASP.NET Project"** window, select **"Empty"** and click **"OK"**

	![Empty Web Project](images/06empty-web-project.png?raw=true)

 - In the **Solution Explorer**, expand the **"LatLon.Hosts.WebHost"**.  Right click **References** and select **"Add Reference..."**.  Expand "Solution" | "Projects" and turn on the checkmarks for the LatLon.Core.Contracts and LatLon.Core.Services projects

	![Solution Project References](images/09solution-project-references.png?raw=true)

 - In the **Solution Explorer**, right click the **LatLon.Hosts.WebHost** project and select **"Add"** | **"New Item..."**.  Select "WCF Service", name the new service **"LatLonUtilities.svc"** and click **"Add"** 

	![Add Wcf Service](images/07add-wcf-service.png?raw=true)

 - Along with the LatLonUtilities.svc file, we got a few files that we don't actually need because we already have them in the LatLon.Core.Contracts.ILatLonUtilitiesService.cs and LatLon.core.Services.LatLonUtilitiesService.cs files.  

 - First, open and **review** the **ILatLonUtilites.cs** and **LatLonUtilites.svc.cs** files.  Explain that these file implement a basic LatLonUtilities service.  However, we already have the ILatLonUtilities contract, and the LatLonUtilitiesService implementation so we don't need them.  Once you have reviewed the files, **delete them**. 

	![Remote Default Service Files](images/08remote-default-service-files.png?raw=true)

 - Next, open the LatLonUtilities.svc file, and modify the markup to match:

````XML
<%@ ServiceHost Service="LatLon.Core.Services.LatLonUtilitiesService"  %>
````

 - Explain that the **".svc"** file extension and **&lt;%@ ServiceHost ...%&gt;** tag instruct ASP.NET to generate the Service host.  It will be hosted on an endpoint that has
  - **Address** - Web site url plus .svc file name (http://lanlon.azurewebsites.net/LatLonUtilities.svc) 
  - **Binding** - BasicHttpBinding by default, although you change that in the configuration files
  - **Contract** - The ILatLonUtilitiesService implemented in the LatLonUtilitiesService class. 

 - Right click the LatLonUtilities.svc file and select "View in Browser (Internet Explorer)".  This should launch the service details page.  You can click either of the links at the top to view the WSDL documents for the service.  

 - Copy the URL for the service out of the browser address window. For me, its: [http://localhost:9271/LatLonUtilities.svc](http://localhost:9271/LatLonUtilities.svc) 

 - As before, update the Win81Client LatLonWcf service reference to point the service now hosted in a local website.  

 - In the **Solution Explorer** window, select the  **LatLon.Clients.Win81Client** project, and from the menu bar select **"Debug"** | **"Start Without Debugging"** or press **Ctrl+F5** to run the Windows 8.1 client app.  Click the **Get Distances** button, and verify that the distances are returned.  


<!-- ======================================================================= -->
<a name="CreateSiteInPortal" />
### Create a Web Site in the Azure Management Portal ###
<!-- ======================================================================= -->
---

You can either create a site ahead of time in the Azure Management Portal, or you can create the site on the fly using the Azure Tools in Visual Studio.  In this quick demo, we'll create a Web Site using the Azure management Portal.

 - Open the [Azure Management Portal](https://manage.windowsazure.com) (https://manage.windowsazure.com) in the browser. 
 - Quickly review the menu of services along the left hand side
 - Click **"WEB SITES"** to see the current web sites for the account
 - Click the **"+ NEW"** button and select **"COMPUTE"** | **"WEB SITE"** 
 - Review the three options:
  - **QUICK CREATE** - Create an empty web site very quickly.  Just a name and region are needed
  - **CUSTOM CREATE** - Create a new site with a name, region, database, and source control integration
  - **FROM GALLERY** - Create a new site using one of the many templates.   
 
 - Choose **QUICK CREATE** and create new site with a name like "LatLonDemo" (your name will have to be unique), and in an appropriate region.  

 - Wait for the new site to be created and show it in the portal.   

	![Web Site in Portal](images/9.5web-site-in-portal.png?raw=true)



<!-- ======================================================================= -->
<a name="PublishWindowsAzureWebSite" />
### Publish to a Windows Azure Web Site###
<!-- ======================================================================= -->
---

Next, we'll take the Web Site we just created and publish it to an Azure Web Site.  

 - Right click the "LatLon.Hosts.WebHost" project, and select "Publish...".  
 - From the **"Publish Web"** window, click the **"Import"** button and either sign into Windows Azure using your Microsoft Account, or use the "Import subscriptions" link to import a publish profile.
 - Once signed in, in the **"Import Publish Settings"** window, click the drop down and show that the Web Site we created in the portal previously appears in the list:

	![Web Site in List](images/9.75web-site-in-list.png?raw=true)

 - **DON'T PUBLISH TO THE SITE WE ALREADY CREATED!!**.  We'll create a new one instead to show that a site can be created in Visual Studio as well.  

 - Click the **"New"** button and complete the details in the **"Create site on Windows Azure"** dialog to create a new site. (Select **"No Database"** for the database server) and click **"Create"**:

	![Create Azure Web Site](images/10create-azure-web-site.png?raw=true)
 
 - Back in the **"Import Publish Settings"** click **"OK"**:

	![Import Publish Settings](images/11import-publish-settings.png?raw=true)

 - Complete the **"Publish Web"** wizard using the defaults. 

 - ***It should only take a minute or two to publish to Azure***.  Once complete, add "LatLonUtilities.svc" to the URL for the web site URL (e.g.: http://lanlon.azurewebsites.net/LatLonUtilities.svc), and verify that the Service details page is shown.

	![Service Running in Web Site](images/11.5service-running-in-web-site.png?raw=true)

 - Copy the URL for the web service, and again, in Visual Studio, configure the LatLonWcf service reference to point to the URL of the hosted service:

	![Configure 8.1 Client To Reference Web Host](images/12configure-81-client-to-reference-web-host.png?raw=true)

 - In the **Solution Explorer**, select the **LatLon.Clients.Win81Client** project, and from the menu bar select **"Debug"** | **"Start Without Debugging"** or press **Ctrl+F5**

 - Click the **"Get Distances"** button and verify that the data is loaded from the web service successfully

 - Cool!  You now have a Web Service hosted in an Azure Web Site, and a Windows 8.1 Client that is using it!  Notice how easy it was to move it into a website.  We didn't have to write any code.  We just leveraged the existing **Contract** (**LonLon.Core.Contracts.ILatLonUtilitiesService**) and **Service** (**LonLon.Core.Services.LatLonUtilitiesService**), and the ASP.NET **".svc"** file type and **&lt;%@ ServiceHost ...%&gt;** tag to host the service.  













## Host the Service in an Azure Web Role ##











<!-- ======================================================================= -->
<a name="CreateCloudServiceAndWebRole" />
### Create the Cloud Service and Web Role Projects ###
<!-- ======================================================================= -->
---

Next, we'll create a Cloud Service project that has a single ASP.NET Web Role.  And show that we can host the service in the Web Role much in the same way we did in the Azure Web Site. 

 - In the **Solution Explorer**, right click the **"LatLon"** solution and select **"Add"** | **"New Project..."**.   Select **Cloud** along the left, then select **"Windows Azure Cloud Service"**.  Name the new project **"LatLon.Hosts.CloudService"** and click **"OK"**.

	![Create Cloud Service](images/13create-cloud-service.png?raw=true)

 - In the **"New Windows Azure Cloud Service"** window, add an **"ASP.NET Web Role"** and name it **"LatLon.Hosts.WebRoleHost"**.  Click **"OK"**:

	![Add Web Role](images/14add-web-role.png?raw=true)

 - In the **"New ASP.NET Project"** window, select **"Empty"** and click **"OK"**:

	![Emtpy Web Role](images/15emtpy-web-role.png?raw=true)

 - **TWO** new projects will be added to the solution in Visual Studio.  The CloudService, and the WebRoleHost.  When you add the Cloud Service to the project, it changes the solution "StartUp Project" to be the cloud service.  Go back into the Solution properties and change it back to **"Current selection"**

 - Expand the **CloudService** project, and show the various **configuration** and **definition** files, as well as the Roles node and settings.  

 - Highlight the http endpoint listening on Port 80.  Explain that it is that endpoint that allows http traffic on port 80 to come into the cloud service and will be load-balanced against all the Web Role instances.  

 - Expand the **WebRole** project, and it's **References**.  Show the various **Microsoft.WindowsAzure**.* references.

 - Open the **WebRole.cs** class file, show that it inherits from **RoleEntryPoint** and review the **OnStart** and possible **Run** and **OnStop** overrides.  Explain that **Run** isn't needed because **IIS** will be hosting our **ASP.NET Application** for us.     


<!-- ======================================================================= -->
<a name="AddWcfServceToWebRole" />
### Add a WCF Service to the Web Role ###
<!-- ======================================================================= -->
---

We'll add a WCF Service to our Web Role JUST like we did for the web site previously. This process is identitical.

 - In the **Solution Explorer", expand the "LatLon.Hosts.WebRoleHost" project, and right click on **References**.  Add references to the LatLon.Core.Contracts and LatLon.Core.Services projects in the solution.  

 - Add a **LatLonUtilities.svc** **WCF Service** to the **LatLon.Hosts.WebRoleHost** project, then **delete** the un-needed **ILatLonUtilities.cs** and **LatLonUtilities.svc.cs** files **JUST** like we did for the web site previously.  

 - Also, just like the web site, modify the **LatLonUtilities.svc** to match the following: 

````XML
<%@ ServiceHost Service="LatLon.Core.Services.LatLonUtilitiesService"  %>
````
 - In the **Solution Explorer**, select the **LatLon.Hosts.CloudService** Project. From the menu bar select **"DEBUG"** | **"Start Without Debugging"** or press **Ctrl+F5**.  You should see the Azure Compute and Storage emulators start, and the browser windows should appear, and say that it can't load a page.  That is because our WebRoleHost project doesn't have a default document.  

 - Add **LatLonUtilities.svc** to the end of the URL in the browser, and verify that the service page loads (eg: http://localhost/LatLonUtilities.svc).  Change any loopback address to **"localhost"** :

	![Service In Web Role Emulator](images/16service-in-web-role-emulator.png?raw=true)

 - Copy the URL from the browser, and back in Visual Studio, re-configure the LatLon.Clients.Win81Client's LatLonWcf service reference to point to the URL of the service running in the compute emulator.

 - Run the LatLon.Clients.Win81Client, click the "Get Distances" button and verify that it works.


<!-- ======================================================================= -->
<a name="PublishTheCloudService" />
### Publish the Cloud Service to Windows Azure ###
<!-- ======================================================================= -->
---

We've built a Cloud Service with a Web Role, and tested it locally in the Compute Emulator. Now, let's publish it into Azure.  The process is very similar to how we publised a Web Site, but there some different choices to make with cloud Services

 - Open the **Windows Azure Management Portal** (https://manage.windowsazure.com) in the browser, and review the current list of Cloud Services.  

 - Mention that you could create a cloud service manually here, but we will create one on the fly as part of the publishing process in Visual Studio.

 - In the **Solultion Explorer**, right-click the **"LatLon.Hosts.CloudService"** project and select **"Publish..."**.  Sign in as needed.  

 - In the **"Publish Windows Azure Application"** window,
  - **"Sign In"**: sign in and select the subscription you want to publish to 
  - **"Settings"**: Create a new cloud service named LatLonDemo (ish) in your region.  Leave all the other options at default.  (You can enable Remote Desktop if you like, and demo it if time allows.  I enabled it for LatLon.cloudapp.app.  devuser/P@ssw1rd).
  - Show, but don't change the advanced settings.
  - **"Summary"**: click **"Publish"**

 - ***The publish process will take some time (5-10minutes)***.  You can review the process in the **"Windows Azure Activity Log"** window in Visual Studio.

 - Once the publish process is complete (or better yet, if you have one pre-published), **navigate to the cloud service url, and to the LatLonUtilities.svc** service (http://latlon.cloudapp.net/LatLonUtilities.svc), and verify that the service page comes up. 

 - In the Solution Explorer, reconfigure the LatLonWcf service reference to point to the service running in the Web Role and verify that it works. 











## Host the Service in an Azure Worker Role ##









<!-- ======================================================================= -->
<a name="AddAWorkerRole" />
### Add a Worker Role to the Cloud Service ###
<!-- ======================================================================= -->
---

We started this demo with our service being hosted by a console application.  Again, recall most any managed application (.NET app) can be a service host.  In this demo, we'll create a Worker Role, which is really a .NET Class library.  We'll write code in the Worker Role's RoleEntryPoint.Run() method to startup a WCF service and keep it running.  

 - In the **Solution Explorer**, expand the **"LatLon.Hosts.CloudService"** project, then **right-click** on the **"Roles"** node, and select **"Add"** | **"New Worker Role Project..."**.  

 - In the **"Add new .NET Framework 4.5 Role Project"** window, select the **"Worker Role"** project template, and name the new project **"LatLon.Hosts.WorkerRoleHost"** and click **"Add"**

	![Add Worker Role](images/17add-worker-role.png?raw=true)

 - Review the contents of the Worker Role project.  Specifically the **References** to the **Microsoft.WindowsAzure**.* assemblies, and the **WorkerRole.cs** class definition and it's **Run** and **OnStart** overrides.

 - **Add references** to the **LatLon.Core.Contracts** and **LatLon.Core.Services** projects as well as to the **System.ServiceModel** assembly

 - We need to add a setting and a couple of endpoints to our Worker role's configuration.  

 - In the **"Solution Explorer"**, expand **"LatLon.Hosts.CloudService"** | **"Roles"** and double click on **"LatLon.Hosts.WorkerRoleHost"** to open it's configuration.   

- Switch to the **"Settings"** page, ensure that the **"Service Configuration"** drop down is set to **"All Configurations"** and click the **"Add Setting"** button.  Name the new setting **"Domain"** and give it a value of **"localhost"**:

	![Add Domain Setting](images/18add-domain-setting.png?raw=true)

 - Next, change the **"Service Configuration"** to **"Cloud"** and change the **"Domain"** setting's value to the fully qualified domain name of the cloud service you published previously (eg: **latlon.cloudapp.net**):

	![Cloud Domain Name](images/19cloud-domain-name.png?raw=true)

 - If you have time, show the changes made to the **ServiceDefinition.csdef** and **ServiceConfiguration.*.cscfg** files for the setting

 - Our Web role came with an http endpoint that listened on port 80.  Worker role's don't get that though.  We need to add an endpoint to allow traffic from outside the Azure Data Center to be forwarded into our Cloud Service and to our WorkerRoleHost instances.  Also, because the Web Role is already listening on port 80, we'll need to have our worker role endpoint on a different port.  

 - Once again, open the settings for the **WorkerRoleHost** by clicking on it's role node in the **Solution Explorer**

 - Switch to the **"Endpoints"**, ensure the **"Service Configuration"** drop down is set to **"All Configurations"** (endpoints are part of the Service Definition, and there is only one version of that), click the **"Add Endpoint"** button and create a new endpoint with the following values:
  - **Name:** WcfTcpPort 
  - **Type:** Input
  - **Protocol:** tcp
  - **Public Port:** 8081

	![Worker Role Endpoint](images/20worker-role-endpoint.png?raw=true)

 - If you change any of the values from those descrbed above make sure the remember the differences so you can modify the code later.  It is recommended that you don't make any changes though to eliminate confusion.  

<!-- ======================================================================= -->
<a name="ImplementWorkerRolehost" />
### Implement the Worker Role Host ###
<!-- ======================================================================= -->
---

We have the configuration updated to meet our needs, now let's write some code.  We'll modify the WorkerRole.Run() method to start a ServiceHost and listen on the domain name and port we just specified in configuraiton files. 

 - In the **"Solution Explorer**, expand the **"LatLon.Hosts.WorkerRoleHost"** project and double click the **"WorkerRole.cs"** code file to open it. All of our changes will take place inside the **Run()** method

````C#
//The initial Run() method code:
public override void Run()
{
  // This is a sample worker implementation. Replace with your logic.
  Trace.TraceInformation("LatLon.Hosts.WorkerRoleHost entry point called", "Information");

  while (true)
  {
    Thread.Sleep(10000);
    Trace.TraceInformation("Working", "Information");
  }
}
````

 - **Replace** the **Trace.TraceInformation** call at the top of the method: 

<!-- strike:1-3 -->
````C#
// This is a sample worker implementation. Replace with your logic.
Trace.TraceInformation("LatLon.Hosts.WorkerRoleHost entry point called", "Information");
````

 - **with**:

````C#
//Report that the Run method has started...
Trace.TraceInformation("LatLon.WorkerRoleServiceHost entry point called");
````

 - **Replace** the **infinite loop** in the **Run()** method with code to create a **ServiceHost** for the **LatLonUtilitiesService**:

````C#
//Create a service host for the LatLon.Core.Services.LatLonUtilitiesService 
using (ServiceHost host = new ServiceHost(typeof(LatLonUtilitiesService)))
{
        
  //Loop, sleep, report, repeat
  while (true)
  {
    Thread.Sleep(30000); //Sleep for 30 seconds....
    Trace.TraceInformation("Listening...");
  }
}
````

 - Just **below the open curly brace of the using** statement, add the following code to **retrieve the domain name from the configuration file**, and to **pre-define the fixed port** that is used externally for the endpoint. 

````C#
//From the configuration files. Domain should be "localhost" when running locally 
//and should be the fqdn of the cloud service when running in the cloud
string domainNameFixed = RoleEnvironment.GetConfigurationSettingValue("Domain");

//This is the "Public Port" of the "WcfTcpPort" input endpoint , and it is exposed 
//by the loadbalancer on the cloud service.  Make sure the value here matches what 
//was given the "WcfTcpPort" "Public Port" in the ServiceDefinition.csdef file. 
int wcfTcpPortFixed = 8081;
````
 - Next, add code to retrieve the **dynamic ip address** and **port** for the **"WcfTcpPort"** on the current WorkerRoleHost instance:

````C#
//Each role instance will have a dynamic ip address, and a dynamic tcp port that the wcf service needs to listen on.  
string ipAddressDynamic = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["WcfTcpPort"].IPEndpoint.Address.ToString();
int wcfTcpPortDynamic = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["WcfTcpPort"].IPEndpoint.Port;
````
 - Next, add code to define and add a Service Endpoint.  Explain the code:

````C#
//Define the fixed URL that client's will use to communicate with the service.  
string serviceEndpointUrl = string.Format("net.tcp://{0}:{1}/LatLonUtilities", domainNameFixed, wcfTcpPortFixed);

//Create a unique url for the service endpoint given this role instance's dynamic ip address and port:
string serviceListenUrl = string.Format("net.tcp://{0}:{1}/LatLonUtilities", ipAddressDynamic, wcfTcpPortDynamic);
        
//Create a Tcp binding that will be used to communicate directly over tcp with no security required (we'll talk about security later).
NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
        
//finally, create an endpoint that uses the ILatLonUtilitiesService Contract, over the tcp Binding using the URLs we just built
host.AddServiceEndpoint(typeof(ILatLonUtilitiesService), tcpBinding, serviceEndpointUrl, new Uri(serviceListenUrl));
````

 - Add code to create a **ServiceMetadataBehavior** and **endpoint** for metadata exchange:

````C#
// Add a metadatabehavior for client proxy generation and add it to he host's behaviors...
ServiceMetadataBehavior metadatabehavior = new ServiceMetadataBehavior();
host.Description.Behaviors.Add(metadatabehavior);

//Create the fixed endpoint for the metadata exchange ("mex") that client's will use
string mexEndpointUrl = string.Format("net.tcp://{0}:{1}/mex", domainNameFixed, wcfTcpPortFixed);

//Create the URL that this rol instance actually listens on give it's dynamic ip address and port
string mexListenUrl = string.Format("net.tcp://{0}:{1}/mex", ipAddressDynamic, wcfTcpPortDynamic);
        
//Create a MexTcpBinding to allow metadata to be exchanged over tcp
Binding mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
        
//And create the mex endpoint, exposing the IMetadataExchange contract over the mex tcp binding, on the urls we just built. 
host.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, mexEndpointUrl, new Uri(mexListenUrl));
````
 - Finally, **open the ServiceHost** and **report the URLs being used** to the the trace listeners:

````C#
//Open the host and start listening!  W00t!
host.Open();

//Report what URLs we are listening on...
Trace.TraceInformation("==========\nListening On:\n==========\n\n{0}\n{1}\n{2}\n{3}\n\n==========",
                        serviceEndpointUrl, serviceListenUrl, mexEndpointUrl, mexListenUrl);
````

 - Just in case, here is the **complete code for the Run() method**:

````C#
public override void Run()
{
  //Report that the Run method has started...
  Trace.TraceInformation("LatLon.WorkerRoleServiceHost entry point called");

  //Create a service host for the LatLon.Core.Services.LatLonUtilitiesService 
  using (ServiceHost host = new ServiceHost(typeof(LatLonUtilitiesService)))
  {
    //From the configuration files. Domain should be "localhost" when running locally 
    //and should be the fqdn of the cloud service when running in the cloud
    string domainNameFixed = RoleEnvironment.GetConfigurationSettingValue("Domain");

    //This is the "Public Port" of the "WcfTcpPort" input endpoint , and it is exposed 
    //by the loadbalancer on the cloud service.  Make sure the value here matches what 
    //was given the "WcfTcpPort" "Public Port" in the ServiceDefinition.csdef file. 
    int wcfTcpPortFixed = 8081;

    //Each role instance will have a dynamic ip address, and a dynamic tcp port that the wcf service needs to listen on.  
    string ipAddressDynamic = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["WcfTcpPort"].IPEndpoint.Address.ToString();
    int wcfTcpPortDynamic = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["WcfTcpPort"].IPEndpoint.Port;

    //Define the fixed URL that client's will use to communicate with the service.  
    string serviceEndpointUrl = string.Format("net.tcp://{0}:{1}/LatLonUtilities", domainNameFixed, wcfTcpPortFixed);

    //Create a unique url for the service endpoint given this role instance's dynamic ip address and port:
    string serviceListenUrl = string.Format("net.tcp://{0}:{1}/LatLonUtilities", ipAddressDynamic, wcfTcpPortDynamic);

    //Create a Tcp binding that will be used to communicate directly over tcp with no security required (we'll talk about security later).
    NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);

    //finally, create an endpoint that uses the ILatLonUtilitiesService Contract, over the tcp Binding using the URLs we just built
    host.AddServiceEndpoint(typeof(ILatLonUtilitiesService), tcpBinding, serviceEndpointUrl, new Uri(serviceListenUrl));

    // Add a metadatabehavior for client proxy generation and add it to he host's behaviors...
    ServiceMetadataBehavior metadatabehavior = new ServiceMetadataBehavior();
    host.Description.Behaviors.Add(metadatabehavior);

    //Create the fixed endpoint for the metadata exchange ("mex") that client's will use
    string mexEndpointUrl = string.Format("net.tcp://{0}:{1}/mex", domainNameFixed, wcfTcpPortFixed);

    //Create the URL that this rol instance actually listens on give it's dynamic ip address and port
    string mexListenUrl = string.Format("net.tcp://{0}:{1}/mex", ipAddressDynamic, wcfTcpPortDynamic);
        
    //Create a MexTcpBinding to allow metadata to be exchanged over tcp
    Binding mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
        
    //And create the mex endpoint, exposing the IMetadataExchange contract over the mex tcp binding, on the urls we just built. 
    host.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, mexEndpointUrl, new Uri(mexListenUrl));

    //Open the host and start listening!  W00t!
    host.Open();

    //Report what URLs we are listening on...
    Trace.TraceInformation("==========\nListening On:\n==========\n\n{0}\n{1}\n{2}\n{3}\n\n==========",
                            serviceEndpointUrl, serviceListenUrl, mexEndpointUrl, mexListenUrl);

    //Loop, sleep, report, repeat
    while (true)
    {
      Thread.Sleep(30000); //Sleep for 30 seconds....
      Trace.TraceInformation("Listening...");
    }
  }
}
````

<!-- ======================================================================= -->
<a name="RunWorkerRoleInEmulator" />
### Run the Worker Role in the Emulator ###
<!-- ======================================================================= -->
---

Now, let's test the Worker Role locally in the emulator. 

 - In the **"Solution Explorer"**, select the **"LatLon.Hosts.CloudService"** project, then from the menu bar select **"DEBUG"** | **"Start Without Debugging"** or press **Ctrl+F5**

 - Once the cloud service is running, from the **System tray**, **right-click** on the **emulator** icon (looks like a Windows logo), and select **"Show Compute Emulator UI"**:

	![Show Emulator UI](images/21show-emulator-ui.png?raw=true)

 - In the **"Windows Azure Compute Emulator (Full)"** window, expand the **LatLon.hosts.WorkerRoleHost** and select it's single instance **0**.  

 - Scroll in the trace output window as needed to find the list of URLs that are being listened on, and copy the URL for the **net.tcp://localhost:8081/mex** endpoint

	![Worker Role in Emulator](images/22worker-role-in-emulator.png?raw=true)

 - Keep the compute emulator running, and update the **Win81Client**'s **LatLonWcf** service reference to point to the URL you just copied.  Then run the Win81Client app and ensure that it can successfully call the service hosted in the Worker Role. 


<!-- ======================================================================= -->
<a name="RePublishCloudService" />
### Re-Publish the Cloud Service to Azure ###
<!-- ======================================================================= -->
---

Once we have the worker role successfully running locally, we can publish it up into Azure.  Since we have already published the Cloud Service before, we can choose to either publish OVER the existing deployment, or we could publish to the "Staging" slot,and do a VIP swap.  To keep things simple, we will publish OVER the existing deployment and replace it with the new one.

 - In the **"Solution Explorer"**, **right click** the **"LatLon.Hosts.CloudService"** project and select **"Publish..."**

 - In the **"Publish Windows Azure Application"** window, accept the defaults (these are the same settings we used when we published previously, so everything should be fine) and click the **"Publish"** button.

 - In the **"Deployment Environment In Use"** window, click the **"Replace"** button to verify the overwriting of the existing deployment.  

	![Replace Confirmation](images/23replace-confirmation.png?raw=true)

 - Watch the status of the deployment in the Visual Studio **"Windows Azure Activity Log"** window until it is complete (5-10mins), or if you already have one published you can move on.  

 - **Update** the **Win81Client** project's **LatLonWcf** service reference with the URL to the *.cloudapp.net (eg: latlon.cloudapp.net) domain, port 8081, mex endpoint.  (eg: **net.tcp://latlon.cloudapp.net:8081/mex**) 

 - **Run the Win81Client** and verify it works!  

 - **WHEW!  DONE!**  


