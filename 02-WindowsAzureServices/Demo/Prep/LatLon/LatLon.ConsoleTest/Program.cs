using LatLon.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.ConsoleTest
{
  class Program
  {
    static void Main(string[] args)
    {

      GeoPosition home = new GeoPosition 
      {
        Latitude = new LatitudeCoordinate(33.027802422345694),
        Longitude = new LongitudeCoordinate(-116.80002361536026)
      };

      GeoPosition work = new GeoPosition
      {
        Latitude = new LatitudeCoordinate(32.87611182572401),
        Longitude = new LongitudeCoordinate(-117.20739752054214)
      };


      LatLonCalculationsService svc = new LatLonCalculationsService();

      double distance = svc.DistanceBetweenTwoPoints(home, work);

      Console.WriteLine("Home: {0}", home);
      Console.WriteLine("Work: {0}", work);
      Console.WriteLine("Distance: {0}",distance);


    }
  }
}
