using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.Contracts
{
  [DataContract]
  public class GeoCoordinate
  {
    protected int degrees = 0;

    /// <summary>
    /// The whole (integer, not decimal) degrees for the coordinate
    /// </summary>
    [DataMember]
    public int Degrees
    {
      get { return degrees; }
      set
      {
        if (degrees != value)
        {
          degrees = value;
          SetDecimalFromDMS();
        }
      }
    }

    protected int minutes = 0;

    /// <summary>
    /// The whole (integer, not decimal) mintues for the coordinate
    /// </summary>
    [DataMember]
    public int Minutes
    {
      get { return minutes; }
      set
      {
        if (minutes != value)
        {
          minutes = value;
          SetDecimalFromDMS();
        }
      }
    }

    protected double seconds = 0;

    /// <summary>
    /// The whole (integer, not decimal) seconds for the coordinate
    /// </summary>
    [DataMember]
    public double Seconds
    {
      get { return seconds; }
      set
      {
        if (seconds != value)
        {
          seconds = value;
          SetDecimalFromDMS();
        }
      }
    }

    protected string direction;
    /// <summary>
    /// The direction ("+" or "-") of the coordinate
    /// </summary>
    [DataMember]
    public string Direction
    {
      get { return direction; }
      set
      {
        if (direction != value)
        {
          direction = value;
          SetDecimalFromDMS();
        }
      }
    }

    protected double decimalDegrees = 0.0d;

    /// <summary>
    /// The Decimal Degrees value (whole degrees with minutes and seconds represented as fractional values) of the coordinate
    /// </summary>
    [DataMember]
    public double DecimalDegrees
    {
      get { return decimalDegrees; }
      set
      {
        if (decimalDegrees != value)
        { 
          decimalDegrees = value;
          SetDMSFromDecimal();
        }
      }
    }

    public GeoCoordinate() : this(0d)
    { }

    public GeoCoordinate(double DecimalDegrees)
    {
      this.decimalDegrees = DecimalDegrees;
      SetDMSFromDecimal();
    }

    public GeoCoordinate(int Degrees, int Minutes, double Seconds, string Direction)
    {
      this.degrees = Degrees;
      this.minutes = Minutes;
      this.seconds = Seconds;
      this.direction = Direction;
      SetDecimalFromDMS();
    }

    /// <summary>
    /// Sets the degrees, minutes and seconds fields based on the decimalDegrees field
    /// </summary>
    protected virtual void SetDMSFromDecimal()
    {
      double absDecDeg = Math.Abs(decimalDegrees);

      direction = (decimalDegrees >= 0) ? "+" : "-";
      //Allow only whole degrees
      degrees = (int)Math.Truncate(absDecDeg);
      //Allow only whole minutes
      minutes = (int)Math.Truncate((absDecDeg - degrees) * 60);
      //Allow fractional seconds
      seconds = (((absDecDeg - degrees) * 60) - minutes) * 60;

    }

    /// <summary>
    /// Sets the decimalDegrees field based on the degrees, minutes and seconds fields
    /// </summary>
    protected virtual void SetDecimalFromDMS()
    {

      int polarity = (direction == "+" || direction == "N" || direction == "E" ) ? +1 : -1;
      decimalDegrees = polarity * (degrees + ((minutes + (seconds / 60.0d)) / 60.0d));

    }

    public override string ToString()
    {
      return string.Format("{0} {1}° {2}' {3}\"", direction, degrees, minutes, seconds);
    }

    public virtual string ToDecimalString()
    {
      return this.DecimalDegrees.ToString();
    }

  }
}
