using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.Core.Contracts
{
  [ServiceContract(Namespace = "http://BretStateham.com/samples/2013/10/LatLon")]
  public interface ILatLonUtilitiesService
  {

    [OperationContract]
    double RadiansBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

    [OperationContract]
    double NauticalMilesBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

    [OperationContract]
    double KilometersBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

    [OperationContract]
    double MilesBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2);

  }
}
