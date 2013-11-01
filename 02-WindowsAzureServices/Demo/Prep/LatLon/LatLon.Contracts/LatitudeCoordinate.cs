using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.Contracts
{
  public class LatitudeCoordinate : GeoCoordinate
  {

    public LatitudeCoordinate()
      : base()
    { }

    public LatitudeCoordinate(double DecimalDegrees)
      : base(DecimalDegrees)
    { }

    public LatitudeCoordinate(int Degrees, int Minutes, double Seconds, string Direction) : base(Degrees,Minutes,Seconds,Direction)
    { }

    protected override void SetDMSFromDecimal()
    {
      base.SetDMSFromDecimal();
      direction = (direction == "+") ? "N" : "S";
    }

  }
}
