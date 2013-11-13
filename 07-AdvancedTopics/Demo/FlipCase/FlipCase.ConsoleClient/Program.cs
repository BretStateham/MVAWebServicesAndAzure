using FlipCase.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FlipCase.ConsoleClient
{
  class Program
  {
    static void Main(string[] args)
    {
      EndpointAddress ep = new EndpointAddress("http://localhost:8080/flipcase/basic");

      IFlipCaseService proxy = ChannelFactory<IFlipCaseService>.CreateChannel(new BasicHttpBinding(), ep);

      StringData stringData = new StringData() { OriginalString = "Hello, FlipCase!" };
      StringData result = proxy.FlipTheCase(stringData);

      Console.WriteLine("Result: {0}=>{1}",result.OriginalString,result.FlippedCaseString);
      Console.WriteLine("Press <ENTER> to terminate the client.");
      Console.ReadLine();

    }
  }
}
