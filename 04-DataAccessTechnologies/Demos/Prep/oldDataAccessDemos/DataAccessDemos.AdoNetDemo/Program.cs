using System;
using System.Data.SqlClient;

namespace DataAccessDemos.AdoNetDemo
{
  class Program
  {

    static string connectionString = "Server=tcp:d9dzgns4nb.database.windows.net,1433;Database=Positions;User ID=sqladmin@d9dzgns4nb;Password=Pa$$w0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

    static void Main(string[] args)
    {
      SqlDataReaderDemo();
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
