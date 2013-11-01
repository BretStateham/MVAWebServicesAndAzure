using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.Contracts
{
    [ServiceContract(Namespace="http://BretStateham.com/samples/2013/10/LatLon")]
    public interface ILatLonCalculationsService
    {
      [OperationContract]
      double DistanceBetweenTwoPoints(GeoPosition Position1, GeoPosition Position2);
      
    }
}
