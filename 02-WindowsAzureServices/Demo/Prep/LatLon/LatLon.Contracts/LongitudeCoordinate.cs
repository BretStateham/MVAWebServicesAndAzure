using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.Contracts
{
  public class LongitudeCoordinate : GeoCoordinate
  {

    public LongitudeCoordinate()
      : base()
    { }

    public LongitudeCoordinate(double DecimalDegrees)
      : base(DecimalDegrees)
    { }

    public LongitudeCoordinate(int Degrees, int Minutes, double Seconds, string Direction)
      : base(Degrees, Minutes, Seconds, Direction)
    { }

    protected override void SetDMSFromDecimal()
    {
      base.SetDMSFromDecimal();
      direction = (direction == "+") ? "E" : "W";
    }
  }
}
