using FlipCase.Contracts;
using FlipCase.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace FlipCase.ConsoleHost
{
  class Program
  {
    static void Main(string[] args)
    {
      using(ServiceHost host = new ServiceHost(typeof(FlipCaseService), new Uri("http://localhost:8080/flipcase")))
      {
        host.AddServiceEndpoint(typeof(IFlipCaseService), new BasicHttpBinding(), "basic");

        //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
        //smb.HttpGetEnabled = true;
        //host.Description.Behaviors.Add(smb);

        host.Open();
                
        var uris = from cd in host.ChannelDispatchers
                   select cd.Listener.Uri.AbsoluteUri;

        Console.WriteLine("HelloIndigoService started and is listenting on the following URIs:");
        foreach (string uri in uris)
          Console.WriteLine("  " + uri);

        Console.WriteLine();

        Console.WriteLine("Press <ENTER> to terminate the service.");
        Console.ReadLine();
      }
    }
  }
}
