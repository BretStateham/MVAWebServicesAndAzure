using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdoNetDemo
{
  public partial class _default : System.Web.UI.Page
  {
    string constr = "Server=tcp:djdrwgqf0p.database.windows.net,1433;Database=Positions;User ID=sqladmin@djdrwgqf0p;Password=Pa$$w0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        //PopulateGridWithReader();
        //PopulateGridWithDataTable();
        PopulateGridWithDataSet();
      }
       
    }

    private void PopulateGridWithDataSet()
    {
      TitleLabel.Text = "Positions from DataSet:";
      using (SqlConnection connection = new SqlConnection(constr))
      {
        using (SqlCommand command = new SqlCommand("SELECT TOP 10 PositionID, ReportedAt, Latitude, Longitude FROM dbo.Positions;", connection))
        {
          using (SqlDataAdapter adapter = new SqlDataAdapter(command))
          {
            using (DataSet dataSet = new DataSet())
            {
              adapter.Fill(dataSet,"Positions");
              PositionsGrid.DataSource = dataSet.Tables["Positions"];
              PositionsGrid.DataBind();
            }
          }
        }
      }
    }

    private void PopulateGridWithDataTable()
    {
      TitleLabel.Text = "Positions from DataTable:";
      using (SqlConnection connection = new SqlConnection(constr))
      {
        using (SqlCommand command = new SqlCommand("SELECT TOP 10 PositionID, ReportedAt, Latitude, Longitude FROM dbo.Positions;", connection))
        {
          using(SqlDataAdapter adapter = new SqlDataAdapter(command))
          {
            using(DataTable table = new DataTable())
            {
              adapter.Fill(table);
              PositionsGrid.DataSource = table;
              PositionsGrid.DataBind();
            }
          }
        }
      }
    }

    private void PopulateGridWithReader()
    {
      TitleLabel.Text = "Positions from DataReader:";
      using (SqlConnection connection = new SqlConnection(constr))
      {
        System.Console.WriteLine(connection.State.ToString());
        using (SqlCommand command = new SqlCommand("SELECT TOP 10 PositionID, ReportedAt, Latitude, Longitude FROM dbo.Positions;",connection))
        {
          connection.Open();
          using (SqlDataReader rdr = command.ExecuteReader())
          {
            PositionsGrid.DataSource = rdr;
            PositionsGrid.DataBind();
          }
        }
      }
    }
  }
}