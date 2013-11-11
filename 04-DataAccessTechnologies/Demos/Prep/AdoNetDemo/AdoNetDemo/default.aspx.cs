using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdoNetDemo
{
  public partial class _default : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if(!IsPostBack)
        PopulateGrid();
    }

    private void PopulateGrid()
    {
      string constr = "Server=tcp:djdrwgqf0p.database.windows.net,1433;Database=Positions;User ID=sqladmin@djdrwgqf0p;Password=Pa$$w0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
      using(SqlConnection connection = new SqlConnection(constr))
      {
        System.Console.WriteLine(connection.State.ToString());
        using (SqlCommand cmd = new SqlCommand("SELECT TOP 10 PositionID, ReportedAt, Latitude, Longitude FROM dbo.Positions;",connection))
        {
          connection.Open();
          using (SqlDataReader rdr = cmd.ExecuteReader())
          {
            PositionsGrid.DataSource = rdr;
            PositionsGrid.DataBind();
          }
        }
      }
    }
  }
}