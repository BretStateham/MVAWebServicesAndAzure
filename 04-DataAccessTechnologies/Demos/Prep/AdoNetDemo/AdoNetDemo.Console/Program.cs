using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo.Console
{
  class Program
  {
    static void Main(string[] args)
    {
      string constr = "Server=tcp:djdrwgqf0p.database.windows.net,1433;Database=Positions;User ID=sqladmin@djdrwgqf0p;Password=Pa$$w0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
      using (SqlConnection connection = new SqlConnection(constr))
      {
        System.Console.WriteLine(connection.State.ToString());
        string query = "SELECT TOP 10 PositionID, ReportedAt, Latitude, Longitude FROM dbo.Positions;";
        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
          connection.Open();
          using (SqlDataReader rdr = cmd.ExecuteReader())
          {
            while (rdr.Read())
            {
              string position = 
                string.Format("{0},{1},{2},{3}", rdr.GetInt32(0), rdr.GetDateTime(1), rdr.GetFloat(2), rdr.GetFloat(3));
              System.Console.WriteLine(position);
            }
          }
        }
      }
      System.Console.WriteLine("Press <ENTER> to continue...");
      System.Console.ReadLine();
    }
  }
}
