<a name="Title" />
# Entity Framework Demo Snippets #
---

##FOR ALL DEMOS, MAKE SURE YOU ARE RUNNING ON WINDOWS 8.1, AND VISUAL STUDIO 2013 WITH THE WINDOWS AZURE SDK 2.2 OR LATER.  YOU MUST ALSO RUN VISUAL STUDIO 2013 AS ADMINISTRATOR##

#### Database First Demo: ####

 - [Open the Demo Project](#OpenDemoProject)
 - [Add Entity Model Using Designer](#AddModelInDesigner)
 - [Consume from WPF](#ConsumeFromWPF)

#### Code First Demo: ####

 - [Add a Position Class to EFDemos.CodeFirst](#AddPositionClass)
 - [Add Entity Framwork from NuGet](#AddEntityFrameworkFromNuGet)
 - [Add the PositionsContenxt DbContext Class](#AddPositionsContextClass)
 - [Consume the Code First Model from WPF](#ConsumeCodeFirstModelFromWPF)

#### WCF Data Services Demo: ####

 - [Create Model In Designer Again](#CreateModelInDesignerAgain)
 - [Add the Microsoft.OData.EntityFrameworkProvider Pre-Release NuGet Package](#AddMicrosoftODataEntityFrameworkProviderPackage)
 - [Consume the WCF Data Service in the Browser](#ConsumeWcfDataServiceInBrowser)
 - [Consume the WCF Data Service in the WPF Client](#ConsumeWcfDataServiceInWPFClient)

---











## Database First Demo ##










<!-- ======================================================================= -->
<a name="OpenDemoProject" />
### Open the Demo Project ###
<!-- ======================================================================= -->
---

For this demo, there is a pre-written demo project.  

 - Open the "\Demos\Begin\EFDemos\EFDemos.sln" solution in Visual Studio 2013

 - Review the contents of the solution

  - EFDemos.DatabaseFirst

  - EFDemos.CodeFirst

  - EFDemos.WcfDataSvc

  - EFDemos.WcfDataClient

 - Open the EFDemos.DatabaseFirst project and review it's contents:

  - MainPage.xaml has the basic UI.   A Button and a List View.
 
  - MainPage.xaml.cs has no code other than an empty event handler. 

<!-- ======================================================================= -->
<a name="AddModelInDesigner" />
### Add Entity Model Using Designer ###
<!-- ======================================================================= -->
---

Add an Entity Data Model to the project using the designer.

 - Right-click the EFDemos.DatabaseFirst Project, and select "Add" | "New Item..."

 - Pick "Data" | "ADO.NET Entity Data Model".  Name the model PositionsEntityModel.edmx
  - Generate from Database
  - Connect to your Azure SQL Positions Database (re-create using the method from the previous lab if needed) 
  - Let it save the password in the conneciton string. 
  - Name the connection string "PositionsContext".  This also becomes the name of the DbContext.
  - Choose Entity Framwork 6.0
  - Include all the tables, and leave the pluralize, etc. at defaults.

 - Rename "Cruis" to "Cruise", reposition entities and do a build.  
 - Review the generated code, etc. 

<!-- ======================================================================= -->
<a name="ConsumeFromWPF" />
### Consume from WPF ###
<!-- ======================================================================= -->
---

Write code to use the Entity Framework

 - In the EFDemos.DatabaseFirst project, open MainPage.xaml.cs.  

 - Modify the GetPositionsButton_Click event handler to match:

````C#

private void GetPositionsButton_Click(object sender, RoutedEventArgs e)
{
  PositionsContext ctx = new PositionsContext();

  var positions = (from p in ctx.Positions
                    where p.CruiseID == 1
                    orderby p.ReportedAt
                    select p).Take(5);

  PositionsList.ItemsSource = positions.ToList();
}

````



 - Run the EFDemos.DatabaseFirst project and ensure the data displays.  











## Code First Demo ##











<!-- ======================================================================= -->
<a name="AddPositionClass" />
### Add a Position Class to EFDemos.CodeFirst ###
<!-- ======================================================================= -->
---

First, well add a "Plain Old CLR Class" (POCO) class to the CodeFirst demo project

 - Open the EFDemos.CodeFirst project in Solution Explorer

 - Add a new class file named Position.cs

 - Add the following code to the class definition:

````C#

namespace EFDemos.CodeFirst
{
  public class Position
  {
    public int PositionID { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
  }
}

````


 - Do a Build to make sure everything is working

<!-- ======================================================================= -->
<a name="AddEntityFrameworkFromNuGet" />
### Add Entity Framwork from NuGet ###
<!-- ======================================================================= -->
---

Add the Entity Framework package from NuGet

 - Right-click the EFDemos.CodeFirst project in the Solution Explorer, and select "Manage NuGet Packages..."

 - Search for Online for "EntityFramework" and click "Install"

<!-- ======================================================================= -->
<a name="AddPositionsContextClass" />
### Add the PositionsContenxt DbContext Class ###
<!-- ======================================================================= -->
---

Add a PositionsContext class that derives from the DbContext base class

 - Add a new class to the project named PositionsContext.cs

 - Add the following code to the class.  

````C#

using System.Data.Entity;

namespace EFDemos.CodeFirst
{
  public class PositionsContext : DbContext
  {
    public DbSet<Position> Positions { get; set; }
  }
}

````


<!-- ======================================================================= -->
<a name="ConsumeCodeFirstModelFromWPF" />
### Consume the Code First Model from WPF ###
<!-- ======================================================================= -->
---

Add code to MainPage.xaml.cs to consume the model...

 - Open MainPage.xaml.cs 

 - Modify the AddPosition_Click event handler to match:

````C#

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

````

 - Set EFDemos.CodeFirst as the startup project, and debug to confirm it works. 











## WCF Data Services Demo ##












<!-- ======================================================================= -->
<a name="CreateModelInDesignerAgain" />
### Create Model In Designer Again ###
<!-- ======================================================================= -->
---

In the EFDemos.WcfDataSvc project, create an ADO.NET Entity Data Model using the designer EXACTLY like we did in the Database First demo.  

 - Make sure to fix up "Cruise"
 - Make sure to name the connection string "PositionsContext"

<!-- ======================================================================= -->
<a name="AddMicrosoftODataEntityFrameworkProviderPackage" />
### Add the Microsoft.OData.EntityFrameworkProvider Pre-Release NuGet Package ###
<!-- ======================================================================= -->
---

WCF Data Service 5.6 needs some help to create a DataService on a DbContext rather than an ObjectContext.  Currently (as of 11/13/2013) this is done by installing the icrosoft.OData.EntityFrameworkProvider Pre-Release NuGet Package.

 - Right-click the EFDemos.WcfDataSvc project and select "Manage NuGet Packages"

 - Make sure to select "Include Prerelease" (not "Stable") from the drop down at the top of the window

 - Search Online for Microsoft.OData.EntityFrameworkProvider

 - Select the "WCF Data Services Entity Framework Provider" and click Install.

 - Add a new "WCF Data Service 5.6" file to the project named PositionsService.svc

 - Modify the code to match:

````C#

using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;

namespace EFDemos.WcfDataSvc
{
    public class PositionsService : EntityFrameworkDataService< PositionsContext >
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
             config.SetEntitySetAccessRule("*", EntitySetRights.All); //Don't do this!  Be more restrictive!
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}

````

 - Do a build, and fix any errors. 

<!-- ======================================================================= -->
<a name="ConsumeWcfDataServiceInBrowser" />
### Consume the WCF Data Service in the Browser ###
<!-- ======================================================================= -->
---

Test the service out in the browser...

 - Set the EFDemos.WcfDataSvc project to be startup project

 - Set the PositionsService.svc to be the startup page

 - Debug the service.  

 - The browser should open, and a default service result should display

 - Try some OData queries (Fix the port):

  - http://localhost:<PORT>/PositionsService.svc/Cruises
  - http://localhost:<PORT>/PositionsService.svc/Positions(1)
  - http://localhost:<PORT>/PositionsService.svc/Positions(1)?$expand=Place
  - http://localhost:<PORT>/PositionsService.svc/Positions?$expand=Place&$filter=Place/AdminName eq 'Bimini'

<!-- ======================================================================= -->
<a name="ConsumeWcfDataServiceInWPFClient" />
### Consume the WCF Data Service in the WPF Client ###
<!-- ======================================================================= -->
---

Consume the WCF Data Service from the WPF Client.

 - Stop debuggin any existing sessions.

 - Open the EFDemos.WcfDataClient Project

 - Right-click "References" and select "Add Service Reference..."

 - Click the dropdown arrow next to "Discove" and select "Services in Solution"

 - The PositionService.svc service should be found.  Copy the URL for it to the clipboard and save it somewhere.  You'll need it in a bit. 

 - Name the service reference PositionsService and click "OK"

 - Update the MainPage.xaml.cs GetPositionsButton_Click event handler to match the following.  Make sure though to update the service URI with the one you copied for the service just a couple of steps back.:

````C#

      PositionsContext ctx = new PositionsContext(new Uri("<PASTE SERVICE URI FROM ABOVE"));

      var positions = (from p in ctx.Positions
                       where p.CruiseID == 1
                       orderby p.ReportedAt descending
                       select p).Take(5);

      PositionsList.ItemsSource = positions.ToList();

````

 - Start a debug session in Visual Studio to re-start the WcfDataSvc project.  Then in the solution explorer, right click the WcfDataClient project and choose "Debug" | "Start new instance"

