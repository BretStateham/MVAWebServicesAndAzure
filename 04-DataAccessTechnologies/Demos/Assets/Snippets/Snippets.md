<a name="Title" />
# !!MODULE TITLE!! Demo Snippets #
---

##FOR ALL DEMOS, MAKE SURE YOU ARE RUNNING ON WINDOWS 8.1, AND VISUAL STUDIO 2013 WITH THE WINDOWS AZURE SDK 2.2 OR LATER.  YOU MUST ALSO RUN VISUAL STUDIO 2013 AS ADMINISTRATOR##

#### Querying Data with ADO.NET: ####

 - [Create Positions Database](#CreatePositionsDatabase)
 - [SqlDataReader Demo](#SqlDataReaderDemo)

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

 - Create a new **Windows Console App** project named **"AdoNetDemos.ConsoleApp"**, name the **solution "AdoNetDemos"**, put it in the **"\Demos\Begin"** folder for this demo. 

 - In the **Solution Explorer**, expand **AdoNetDemos.ConsoleApp** | **References** and show the exsting **System.Data** reference. 

 - Open Program.cs

 - Add a class member named connectionString.  

 - Open the database in the Azure Management portal, and on the Quick Start page (the cloud with the lightning bolt), copy the ADO.NET connection string.

 - Initialize the connectionString variable to the string copied from the portal.  Set the password.  

````C#
static string connectionString = "<paste connection string here>";
````

 - Create a new method named SqlDataReaderDemo() and call it from Main();

 - Add the following code for SqlDataReaderDemo() fixing the <SERVERNAME>, <LOGIN> and <PASSWORD> place holders in the connection string as required:

````C#

namespace AdoNetDemos.ConsoleApp
{
  class Program
  {

    static string connectionString = "Server=tcp:<SERVERNAME>.database.windows.net,1433;Database=Positions;User ID=<LOGIN>@<SERVERNAME>;Password=<PASSWORD>;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

    static void Main(string[] args)
    {
      SqlDataReaderDemo();
    }

    private static void SqlDataReaderDemo()
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using(SqlCommand command = new SqlCommand("SELECT TOP 10 PositionID, ReportedAt, Latitude, Longitude FROM dbo.Positions;",connection))
        {
          connection.Open();
          using(SqlDataReader reader = command.ExecuteReader())
          {
            while(reader.Read())
            {
              Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader[0], reader[1], reader[2], reader[3]);
            }
          }
        }
      }
    }
  }
}

````


<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

<!-- ======================================================================= -->
<a name="TaskTitle" />
### Task Title ###
<!-- ======================================================================= -->
---

Simple Description

 - Step 1

 - Step 2

 - Step 3

