using System;
using System.Linq;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace DataAccessDemos
{
  class Program
  {

    static string connectionString = "Server=tcp:d9dzgns4nb.database.windows.net,1433;Database=Positions;User ID=sqladmin@d9dzgns4nb;Password=Pa$$w0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

    static void Main(string[] args)
    {
      //SqlDataReaderDemo();
      //LINQToObjectsDemo();
      //WalkTheDOMDemo();
      LINQtoXMLDemo();

      Console.WriteLine("\nPress ENTER to continue...");
      Console.ReadLine();
    }

    private static void LINQtoXMLDemo()
    {
      XDocument doc = XDocument.Load("Positions Sample.xml");

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
    }

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

    private static void LINQToObjectsDemo()
    {
      string[] names = { "Jeremy","Michael","Adam","Jerry","Bruno","Bret" };

      var results = from name in names
                    where name.HasEvenLength()
                    orderby name
                    select name;

      foreach (string name in results)
        Console.WriteLine(name);
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
