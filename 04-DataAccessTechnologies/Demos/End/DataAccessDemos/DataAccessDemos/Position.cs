using System;

namespace DataAccessDemos
{
  public class Position
  {
    public int? PositionID { get; set; }
    public int? CruiseID { get; set; }
    public DateTime? ReportedAt { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public override string ToString()
    {
      return string.Format("{0}\t{1}\t{2}\t{3:N9}\t{4:N9}", PositionID, CruiseID, ReportedAt, Latitude, Longitude);
    }
  }
}
