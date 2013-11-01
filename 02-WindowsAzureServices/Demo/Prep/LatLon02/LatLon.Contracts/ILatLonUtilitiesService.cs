using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.Contracts
{

    [ServiceContract(Namespace="http://BretStateham.com/samples/2013/10/LatLon")]
    public interface ILatLonUtilitiesService
    {

      [OperationContract]
      double RadiansBetweenToPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

      [OperationContract]
      double NauticalMilesBetweenToPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

      [OperationContract]
      double KilometersBetweenToPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

      [OperationContract]
      double MilesBetweenToPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

    }
}
