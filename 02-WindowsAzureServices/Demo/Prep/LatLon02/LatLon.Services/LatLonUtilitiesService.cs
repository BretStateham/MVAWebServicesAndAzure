using LatLon.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.Services
{
    public class LatLonUtilitiesService : ILatLonUtilitiesService
    {

      public double RadiansBetweenToPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
      {
        //Using Haversine Forumla: http://en.wikipedia.org/wiki/Haversine_formula 
        //Reference Implementation: http://www.movable-type.co.uk/scripts/latlong.html

        double deg2rad = (Math.PI / 180.0);

        double dLat = (Latitude2 - Latitude1) * deg2rad;
        double dLon = (Longitude2 - Longitude1) * deg2rad;
        double lat1 = Latitude1 * deg2rad;
        double lat2 = Latitude2 * deg2rad;

        double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Pow(Math.Sin(dLon / 2), 2) * Math.Cos(lat1) * Math.Cos(lat2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return c;
      }


      public double NauticalMilesBetweenToPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
      {
        //Convert to Nautical miles by first converting back to degrees, then multiplying by 60NM (Nautical Miles) / Degree
        //distance = (distance * 180 / PI) * 60;
        return RadiansBetweenToPoints(Latitude1, Longitude1, Latitude2, Longitude2) * (180 / Math.PI) * 60;
      }

      public double KilometersBetweenToPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
      {
        //Convert to Kilometers by multiplying the distance in radians by the earth's radius in kilometers, 6371 (see http://en.wikipedia.org/wiki/Earth_radius) 
        //distance *= 6371;
        return RadiansBetweenToPoints(Latitude1, Longitude1, Latitude2, Longitude2) * 6371;
      }

      public double MilesBetweenToPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
      {
        //Convert to Miles by multiplying the distance in radians by the earth's radius in miles, 3959 (see http://en.wikipedia.org/wiki/Earth_radius) 
        //distance *= 3959;
        return RadiansBetweenToPoints(Latitude1, Longitude1, Latitude2, Longitude2) * 3959;
      }

    }
}
