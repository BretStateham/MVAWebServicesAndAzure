using LatLon.Core.Contracts;
using LatLon.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace LatLon.Hosts.ConsoleHost
{
  class Program
  {
    static void Main(string[] args)
    {

      //Create a new ServiceHost intance with a "using" statement.  
      //The using statement makes sure that the host is "Dispose"d even if there
      //is an exception. 
      //The host will offer the LatLonUtilitiesService methods up to it's clients
      //and listen on the http://localhost:8000/LatLon/ URL for service call as well 
      //as metadata requests. This base address will be the root URL for all endpoints.
      using (ServiceHost host = 
              new ServiceHost(
                typeof(LatLonUtilitiesService), 
                new Uri("http://localhost:8000/LatLon/")))
      {

        //A single service can be exposed through any number of "Endpoints". 
        //An endpoint has an Address, a Binding, and a Contract that it supports.  
        //For this example, we'll add a single endpoint that has the following 
        //Contract: And ILatLonUtilitiesService implementation
        //Binding:  BasicHttpBinding - using basic http calls (no, WS*, WSE, Security, etc)
        //Address:  http://localhost:8000/LatLon/LatLonUtilities (host base address + endpoint address)
        host.AddServiceEndpoint(
          typeof(ILatLonUtilitiesService),
          new BasicHttpBinding(),
          "LatLonUtilities");

        //Allow our service to expose metadata to clients.  This metadata 
        //allows client applications on any platform to build their own 
        //proxy classes without having to have access to the original contracts. 
        ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
        smb.HttpGetEnabled = true;
        smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
        host.Description.Behaviors.Add(smb);

        
        //Listen for metadata requests using an endpoint with:
        //Contract:  ServiceMetadataBehavior.MexContractName (IMetadataExchange)
        //Binding:   MetadataExchangeBindings.CreateMexHttpBinding() (A WSHttpBinding with no security)
        //Address:   http://localhost:8000/LatLon/mex (host base address + endpoint address)
        host.AddServiceEndpoint(
          ServiceMetadataBehavior.MexContractName,
          MetadataExchangeBindings.CreateMexHttpBinding(),
          "mex");

        
        //Start the service, and begin listening for requests
        host.Open();

        //Show the addresses that the host is listening on...
        StringBuilder sb = new StringBuilder(
          "======================================================================\n" + 
          "Listening On:\n" +
          "======================================================================\n\n");
        foreach(var cd in host.ChannelDispatchers)
        {
          sb.AppendFormat("  {0} ({1})\n", cd.Listener.Uri.ToString(),cd.State.ToString());
        }
        sb.Append(
          "\n======================================================================");
        Console.WriteLine(sb.ToString());

        //Keep the service running until the user presses the <Enter> key
        Console.WriteLine("Service Started...");
        Console.WriteLine("Press <ENTER> to terminate:");
        Console.ReadLine();

        //Stop the service
        Console.WriteLine("Shutting Down...");
        host.Close();
        Console.WriteLine("Done...");

      }
    }
  }
}
