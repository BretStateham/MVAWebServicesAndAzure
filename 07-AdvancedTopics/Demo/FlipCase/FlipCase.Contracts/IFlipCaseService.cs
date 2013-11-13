using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FlipCase.Contracts
{
  [ServiceContract]
  public interface IFlipCaseService
  {

    [OperationContract]
    StringData FlipTheCase(StringData sd);
  }
}
