using LatLon.ConsoleServiceClient.LatLonWcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.ConsoleServiceClient
{
  class Program
  {
    static void Main(string[] args)
    {

      // Get Lat Lon: http://getlatlon.yohman.com/
      // Convert DMS to DEC to DMS: http://transition.fcc.gov/mb/audio/bickel/DDDMMSS-decimal.html
      // Dec / DMS / http://www.gps-coordinates.net/
      // Yet another dec/dms converter: http://www.stevemorse.org/jcal/dms.html 
      // Distance Calc: http://www.movable-type.co.uk/scripts/latlong.html 


      double latHome = 33.027802422345694; //N 33° 1' 49.2168"
      double lonHome = -116.80002361536026; //W 116° 47' 58.8438"
      double latWork = 32.87611182572401; //N 32° 52' 34.0026"
      double lonWork = -117.20739752054214; //W 117° 12' 26.6292"

      try
      {
        LatLonUtilitiesServiceClient svc = new LatLonUtilitiesServiceClient();

        Console.WriteLine("Service Client:");
        Console.WriteLine("Home: {0}, {1}", latHome, lonHome);
        Console.WriteLine("Work: {0}, {1}", latWork, lonWork);
        Console.WriteLine("Distance (Rads): {0}", svc.RadiansBetweenToPoints(latHome, lonHome, latWork, lonWork));
        Console.WriteLine("Distance (NM):   {0}", svc.NauticalMilesBetweenToPoints(latHome, lonHome, latWork, lonWork));
        Console.WriteLine("Distance (KM):   {0}", svc.KilometersBetweenToPoints(latHome, lonHome, latWork, lonWork));
        Console.WriteLine("Distance (MI):   {0}", svc.MilesBetweenToPoints(latHome, lonHome, latWork, lonWork));

      }
      catch (Exception ex)
      {
        Console.WriteLine("\nThere was a problem contacting the service!\n{0}\n{1}", ex.GetType().Name, ex.Message);
      }
    }
  }
}
