using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.Contracts
{
  [DataContract]
  public class GeoPosition
  {

    /// <summary>
    /// The GeoCoordinate value for Latitude
    /// </summary>
    [DataMember]
    public LatitudeCoordinate Latitude { get; set; }

    /// <summary>
    /// The GeoCoordinate value for Longitude
    /// </summary>
    [DataMember]
    public LongitudeCoordinate Longitude { get; set; }

    public override string ToString()
    {
      return string.Format("{0}, {1}",Latitude,Longitude);
    }

    public virtual string ToDecimalString()
    {
      return string.Format("{0}, {1}", Latitude.ToDecimalString(), Longitude.ToDecimalString());
    }

  }
}
