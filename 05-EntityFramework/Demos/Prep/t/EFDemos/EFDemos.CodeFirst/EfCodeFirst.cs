using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemos.CodeFirst
{
  public class Position
  {
    public int PositionID { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime ReportedAt { get; set; }
  }

  public class PositionsContext : DbContext
  {
    public DbSet<Position> Positions { get; set; }
  }

  
}
