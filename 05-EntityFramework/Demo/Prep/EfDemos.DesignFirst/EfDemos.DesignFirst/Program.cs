using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDemos.DesignFirst
{
  class Program
  {
    static void Main(string[] args)
    {

      //UseRelationships();
      GetDistinct();

    }

    private static void GetDistinct()
    {
      PositionsEntities ctx = new PositionsEntities();

      var cruiseNames = (from p in ctx.Positions
                         select p.Cruise.Name).Distinct();

      foreach (var name in cruiseNames)
        Console.WriteLine("{0}", name);

    }

    private static void UseRelationships()
    {
      PositionsEntities ctx = new PositionsEntities();

      var cruise1 = from p in ctx.Positions
                    where p.CruiseID == 3
                    select new
                    {
                      p.PositionID,
                      p.Latitude,
                      p.Longitude,
                      p.ReportedAt,
                      p.CruiseID,
                      p.Cruise.Name
                    };

      foreach (var p in cruise1)
        Console.WriteLine("{0}:{1},{2} - {3},{4}", p.PositionID, p.Latitude, p.Longitude, p.ReportedAt, p.Name);
    }
  }
}
