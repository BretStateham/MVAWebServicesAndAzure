<a name="Title" />
# Data Access Demo Snippets #
---

##FOR ALL DEMOS, MAKE SURE YOU ARE RUNNING ON WINDOWS 8.1, AND VISUAL STUDIO 2013 WITH THE WINDOWS AZURE SDK 2.2 OR LATER.  YOU MUST ALSO RUN VISUAL STUDIO 2013 AS ADMINISTRATOR##

#### Querying Data with ADO.NET: ####

 - [Create Positions Database](#CreatePositionsDatabase)
 - [SqlDataReader Demo](#SqlDataReaderDemo)

#### LINQ to Objects Demo: ####

 - [Sort Arrays with LINQ](#SortArraysWithLinq)
 - [Filter Arrays With LINQ](#FilterArraysWithLinq)
 - [LINQ and Extension Methods](#LinqAndExtensionMethods)


#### Process XML with the DOM: ####

 - [Walk the DOM](#WalkTheDOM)
 - [Get XML From SQL Server](#GetXMLFromSQLServer)

#### Process XML with the LINQ to XML: ####

 - [Process XML with LINQ](#ProcessXMLWithLINQ)
 - [LINQ to XML Projections](#LINQtoXMLProjections)

---











## Querying Data with ADO.NET ##










<!-- ======================================================================= -->
<a name="CreatePositionsDatabase" />
### Create Positions Database ###
<!-- ======================================================================= -->
---

You need to create the Positions database.  You can do this either on a non-Azure SQL instance of SQL Server or in an Azure SQL Database.  The Assets folder contains appropriate scripts depending on which environment you wish to use.  The demo assumes an Azure SQL Database.  

 - Login to the **Azure Management Portal** and **create a new Azure SQL Database** named **"Positions"**.  You can create the database on an existing server or create a new one, whichever you choose.  

 - If you create a new SQL Server instance, **add a firewall rule to allow connections from your workstation**. 

 - Connect to the Azure SQL Database using **SSMS** from your **development workstation** and run the appropriate **database creation script** from the **"\Assets\Database"** folder:
  - **"Create Position Tables Only (Azure).sql"** if using Azure SQL Database
  - **"Create Position Tables and Database (local).sql"** if using a non-Azure db

 - In **SSMS's "Object Explorer"** window, inspect the structure of the new database.  
	- **Tables**:
		- dbo.Cruises
		- dbo.Places
		- dbo.Positions
		- dbo.TimeZones
	- **Views**:
		- dbo.PositionDetails
	- **Stored Procedures**:
		 - dbo.GetCruiseDetailsProc

<!-- ======================================================================= -->
<a name="SqlDataReaderDemo" />
### SqlDataReader Demo ###
<!-- ======================================================================= -->
---

In this demo, we'll create a quick Console Application, then a Web applicaiton demo to show how ADO.NET can be used to retrieve data...

 - Open Visual Studio 2013

 - Create a new **Windows Console App** project named **"DataAccessDemos"**, put it in the **"\Demos\Begin"** folder for this demo. 

 - In the **Solution Explorer**, expand **DataAccessDemos** | **References** and show the exsting **System.Data** reference. 

 - Open Program.cs

 - Add a class member named connectionString.  

 - Open the database in the Azure Management portal, and on the Quick Start page (the cloud with the lightning bolt), copy the ADO.NET connection string.

 - Initialize the connectionString variable to the string copied from the portal.  Set the password.  

````C#
static string connectionString = "<paste connection string here>";
````

 - Create a new method named SqlDataReaderDemo() and call it from Main();

 - Add code to wait for the user to press enter at the end of Main();

````C#
  Console.WriteLine("Press ENTER to continue...");
  Console.ReadLine();
````


 - Add the following code for SqlDataReaderDemo() fixing the <SERVERNAME>, <LOGIN> and <PASSWORD> place holders in the connection string as required:

````C#

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDemos
{
  class Program
  {

    static string connectionString = "Server=tcp:<SERVERNAME>.database.windows.net,1433;Database=Positions;User ID=<LOGIN>@<SERVERNAME>;Password=<PASSWORD>;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

    static void Main(string[] args)
    {
      SqlDataReaderDemo();

      Console.WriteLine("\nPress ENTER to continue...");
      Console.ReadLine();
    }

    private static void SqlDataReaderDemo()
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlCommand command = new SqlCommand("SELECT TOP 10 PositionID, ReportedAt, Latitude, Longitude FROM dbo.Positions;", connection))
        {
          connection.Open();
          using (SqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              Console.WriteLine("{0}\t{1}\t{2:N9}\t{3:N9}", reader[0], reader[1], reader[2], reader[3]);
            }
          }
        }
      }
    }
  }
}

````











## LINQ to Objects Demo ##












<!-- ======================================================================= -->
<a name="SortArraysWithLinq" />
### Sort Arrays with LINQ ###
<!-- ======================================================================= -->
---

Demonstrate how LINQ can be used to process data in memory.  You will conitnue working with the DataAccessDemos project started in the previous demo. 

 - In Main, comment out the call to SqlDataReaderDemo, and make a call to a new method named LINQToObjectsDemo() and let VS generate the method stub

 - Add the following code to process an array of names in LINQ:

````C#
private static void LINQToObjectsDemo()
{
  string[] names = { "Jeremy","Michael","Adam","Jerry","Bruno","Bret" };
}
````

 - Comment out the "using System.Linq" namespace import at the top, and inspect which methods are avialable on the names array variable. 

 - Uncomment the "using System.Linq" namespace import, and again inspect the list of methods available on the names array variable. 


 - Modify the LINQToObjectsDemo() to match:

````C#
private static void LINQToObjectsDemo()
{
  string[] names = { "Jeremy","Michael","Adam","Jerry","Bruno","Bret" };

  var results = from name in names
                orderby name
                select name;

  foreach(string name in results)
    Console.WriteLine(name);

}
````


<!-- ======================================================================= -->
<a name="FilterArraysWithLinq" />
### Filter Arrays With LINQ ###
<!-- ======================================================================= -->
---

Continue working with the LINQToObjectsDemo method from above. 

 - Add a where clause to include only names with an B in them:
<!-- mark:2 -->
````C#
var results = from name in names
              where name.Contains('B')
              orderby name
              select name;
````


 - First, run the method, and verify that only Bret and Bruno are returned

 - Place a breakpoint on the foreach statement.  Run the app, and step through the execution.  Show that the code steps back up into the LINQ query to evaluate each member

 - Modify the query to first have a query, then re-filter the query with a where extension method. 

````C#
      var results = from name in names
                    select name;

      results = results.Where(n => n.Contains('B')).OrderBy(n => n);
````
 - Again, you can step through the execution to see the compsable query and deferred execution

 - Make a final edit to get rid of the "Comprehension Syntax" query and use purely extension methods:

````C#
var results = names.Where(n => n.Contains('B')).OrderBy(n => n).Select(n => n);
````


<!-- ======================================================================= -->
<a name="LinqAndExtensionMethods" />
### LINQ and Extension Methods ###
<!-- ======================================================================= -->
---

Show how Extension methods can be used to create your own custom operators on types

 - Put the LINQ query back to the original:

````C#
var results = from name in names
              orderby name
              select name;
````

 - Then, modify it to return names that have even lengths...
<!-- mark:2 -->
````C#
var results = from name in names
              where name.Length % 2 == 0
              orderby name
              select name;
````

 - The problem with the query above is that it isn't obvious what is being asked. 

 - We can clarify that with extension methods. 

 - Add a new class file to the project named **StringExtensions.cs** and add the following code for the class:

````C#

namespace DataAccessDemos
{
  public static class StringExtensions
  {
    public static bool HasEvenLength(this string Source)
    {
      return Source.Length % 2 == 0;
    }
  }
}

````

 - Then modify the query to match:
<!-- mark:2 -->
````C#
var results = from name in names
              where name.HasEvenLength()
              orderby name
              select name;
````











## Process XML with the DOM ##












<!-- ======================================================================= -->
<a name="GetXMLFromSQLServer" />
### Get XML From SQL Server ###
<!-- ======================================================================= -->
---

In order to demo processing XML, we need to get some XML.  One cool way to get XML data is from relational data in SQL Server.  

 - In SSMS, connect to the Positions database we created earlier. 

 - Open the **"\Assets\Database\Return Positions as XML.sql"** script, and ensure that you are connected to the database. 

 - Show the queries in the script file.  Run each, and show that the first one returns ALL positions (1183 of them) as XML elements.  The second one returns just a sample (100 at most).  

 - There are pre-made XML files in the **"\Assets\Database"** directory.  They correspond to those two result sets.   

 - In Visual Studio Solution Explorer, right click the "DataAccessDemos" project and select **"Add"** | **"Existing item..."**, then browse to and add the **"Assets\Database\Positions Sample.xml"** file (need to set file filter to include *.xml)

 - With the XML file selected in the Solution Explorer, go to the **"Properties"** window and set the **"Copy to Output Directory"** property to **"Copy if newer"**

<!-- ======================================================================= -->
<a name="WalkTheDOM" />
### Walk the DOM ###
<!-- ======================================================================= -->
---

We'll write some code to process XML by "Walking the DOM".  

 - Comment out the LINQToObjectsDemo() method call in Main()
 - Add, and stub out, a new method call to WalkTheDOMDemo()

 - Add the following code (build up to it if possible, to walk the DOM)

````C#
private static void WalkTheDOMDemo()
{
  XmlDocument doc = new XmlDocument();
  doc.Load("Positions Sample.xml");

  var root = doc.DocumentElement;

  RecurseNode(root,0);

}

private static void RecurseNode(XmlNode Node, int Level)
{
  Console.WriteLine("{0}{1}",new string(' ',Level),Node.Name);

  if(Node.Attributes != null)
    foreach(XmlAttribute attr in Node.Attributes)
      Console.WriteLine("{0}{1}", new string(' ', Level), attr.Name);

  foreach (XmlNode child in Node.ChildNodes)
    RecurseNode(child, Level+1);
}
````
 - Blech! Right?!?











## Process XML with the LINQ to XML ##













<!-- ======================================================================= -->
<a name="ProcessXMLWithLINQ" />
### Process XML with LINQ ###
<!-- ======================================================================= -->
---

Ok, now, let's take a look at LINQ to XML.  So cool.  Wish we had more time to talk about it!  

 - Comment out the WalkTheDOMDemo() method call in Main()
 - Make, and stub out, a call to LINQtoXmlDemo();

 - Add the following code for the LINQtoXmlDemo() method:

````C#
private static void LINQtoXMLDemo()
{
  XDocument doc = XDocument.Load("Positions Sample.xml");

  var cruise2 = from p in doc.Root.Elements("Position")
                where (int?)p.Attribute("CruiseID") == 2
                select p;

}
````
 - Step through the code execution and show the value of cruise2 after it executes.  

 - Tell me that's not cool! Come on!

<!-- ======================================================================= -->
<a name="LINQtoXMLProjections" />
### LINQ to XML Projections ###
<!-- ======================================================================= -->
---

Show how easy it is to use LINQ Projections to turn the XML result from above into a collection of ANONYMOUS objects...

 - Modify the query above to project positions into anonymous types

 - Add a foreach loop to display to the positions in the console:

````C#

var cruise2 = from p in doc.Root.Elements("Position")
              where (int?)p.Attribute("CruiseID") == 2
              select new
              {
                PositionID = (int?)p.Attribute("PositionID"),
                CruiseID = (int?)p.Attribute("CruiseID"),
                ReportedAt = (DateTime?)p.Element("ReportedAt"),
                Latitude = (double?)p.Element("Latitude"),
                Longitude = (double?)p.Element("Longitude")
              };

foreach(var p in cruise2)
  Console.WriteLine("{0}\t{1}\t{2}\t{3:N9}\t{4:N9}", p.PositionID, p.CruiseID, p.ReportedAt, p.Latitude, p.Longitude);

````

 - Time permitting, add a new class to the project name Position.cs.  Here is the code for it:

````C#

using System;

namespace DataAccessDemos
{
  public class Position
  {
    public int? PositionID { get; set; }
    public int? CruiseID { get; set; }
    public DateTime? ReportedAt { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public override string ToString()
    {
      return string.Format("{0}\t{1}\t{2}\t{3:N9}\t{4:N9}", PositionID, CruiseID, ReportedAt, Latitude, Longitude);
    }
  }
}

````

 - Modify the query and projection above to project to the new Position class. 

````C#

var cruise2 = from p in doc.Root.Elements("Position")
              where (int?)p.Attribute("CruiseID") == 2
              select new Position
              {
                PositionID = (int?)p.Attribute("PositionID"),
                CruiseID = (int?)p.Attribute("CruiseID"),
                ReportedAt = (DateTime?)p.Element("ReportedAt"),
                Latitude = (double?)p.Element("Latitude"),
                Longitude = (double?)p.Element("Longitude")
              };

foreach(var p in cruise2)
  Console.WriteLine(p);

````
 
