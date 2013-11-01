using LatLon.Contracts;
using LatLon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.ConsoleServiceHost
{
  class Program
  {
    static void Main(string[] args)
    {

      using (ServiceHost host = new ServiceHost(typeof(LatLonUtilitiesService), new Uri("http://localhost:8000/LatLon")))
      {

        host.AddServiceEndpoint(
          typeof(ILatLonUtilitiesService), 
          new BasicHttpBinding(), 
          "LatLonUtilities");

        ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
        smb.HttpGetEnabled = true;
        smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
        host.Description.Behaviors.Add(smb);

        host.AddServiceEndpoint(
          ServiceMetadataBehavior.MexContractName, 
          MetadataExchangeBindings.CreateMexHttpBinding(), 
          "mex");

        host.Open();

        Console.WriteLine("Service Started...");
        Console.WriteLine("Press <ENTER> to terminate:");
        Console.ReadLine();

        Console.WriteLine("Shutting Down...");
        host.Close();
        Console.WriteLine("Done...");

      }

    }
  }
}
