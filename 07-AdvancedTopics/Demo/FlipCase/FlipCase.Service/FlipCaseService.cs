using FlipCase.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipCase.Service
{
  public class FlipCaseService : IFlipCaseService
  {
    public StringData FlipTheCase(StringData sd)
    {
      sd.FlippedCaseString = null;
      foreach (char c in sd.OriginalString)
      {
        sd.FlippedCaseString += char.IsLower(c) ?
              c.ToString().ToUpper() : c.ToString().ToLower();
      }
      return sd;
    }
  }
}
