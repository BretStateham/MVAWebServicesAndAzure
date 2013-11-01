using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.Contracts
{
  public class LatLonCalculationsService : ILatLonCalculationsService
  {
    public double DistanceBetweenTwoPoints(GeoPosition Position1, GeoPosition Position2)
    {

      double distance = 0;

      double radLat1 = Position1.Latitude.DecimalDegrees * (Math.PI / 180.0);
      double radLon1 = Position1.Longitude.DecimalDegrees * (Math.PI / 180.0);
      double radLat2 = Position2.Latitude.DecimalDegrees * (Math.PI / 180.0);
      double radLon2 = Position2.Longitude.DecimalDegrees * (Math.PI / 180.0);

      double latDelta = radLat2 - radLat1;
      double lonDelta = radLon2 - radLon1;

      // Intermediate result a.
      double a = Math.Pow(Math.Sin(latDelta / 2.0), 2.0) +
                 Math.Cos(radLat1) * Math.Cos(radLat2) *
                 Math.Pow(Math.Sin(lonDelta / 2.0), 2.0);

      // Intermediate result c (great circle distance in Radians).
      double c = 2.0 * Math.Asin(Math.Sqrt(a));

      // Distance.
      // const Double kEarthRadiusMiles = 3956.0;
      const Double kEarthRadiusKms = 6376.5;
      distance = kEarthRadiusKms * c;

      return distance;
    }
  }
}
