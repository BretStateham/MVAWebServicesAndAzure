using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using System.ServiceModel;
using LatLon.Core.Services;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using LatLon.Core.Contracts;

namespace LatLon.Hosts.WorkerRoleHost
{
  public class WorkerRole : RoleEntryPoint
  {
    public override void Run()
    {
      //Report that the Run method has started...
      Trace.TraceInformation("LatLon.WorkerRoleServiceHost entry point called");

      //Create a service host for the LatLon.Core.Services.LatLonUtilitiesService 
      using (ServiceHost host = new ServiceHost(typeof(LatLonUtilitiesService)))
      {
        //From the configuration files. Domain should be "localhost" when running locally 
        //and should be the fqdn of the cloud service when running in the cloud
        string domainNameFixed = RoleEnvironment.GetConfigurationSettingValue("Domain");

        //This is the "Public Port" of the "WcfTcpPort" input endpoint , and it is exposed 
        //by the loadbalancer on the cloud service.  Make sure the value here matches what 
        //was given the "WcfTcpPort" "Public Port" in the ServiceDefinition.csdef file. 
        int wcfTcpPortFixed = 8081;

        //Each role instance will have a dynamic ip address, and a dynamic tcp port that the wcf service needs to listen on.  
        string ipAddressDynamic = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["WcfTcpPort"].IPEndpoint.Address.ToString();
        int wcfTcpPortDynamic = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["WcfTcpPort"].IPEndpoint.Port;

        //Define the fixed URL that client's will use to communicate with the service.  
        string serviceEndpointUrl = string.Format("net.tcp://{0}:{1}/LatLonUtilities", domainNameFixed, wcfTcpPortFixed);

        //Create a unique url for the service endpoint given this role instance's dynamic ip address and port:
        string serviceListenUrl = string.Format("net.tcp://{0}:{1}/LatLonUtilities", ipAddressDynamic, wcfTcpPortDynamic);

        //Create a Tcp binding that will be used to communicate directly over tcp with no security required (we'll talk about security later).
        NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);

        //finally, create an endpoint that uses the ILatLonUtilitiesService Contract, over the tcp Binding using the URLs we just built
        host.AddServiceEndpoint(typeof(ILatLonUtilitiesService), tcpBinding, serviceEndpointUrl, new Uri(serviceListenUrl));

        // Add a metadatabehavior for client proxy generation and add it to he host's behaviors...
        ServiceMetadataBehavior metadatabehavior = new ServiceMetadataBehavior();
        host.Description.Behaviors.Add(metadatabehavior);

        //Create the fixed endpoint for the metadata exchange ("mex") that client's will use
        string mexEndpointUrl = string.Format("net.tcp://{0}:{1}/mex", domainNameFixed, wcfTcpPortFixed);

        //Create the URL that this rol instance actually listens on give it's dynamic ip address and port
        string mexListenUrl = string.Format("net.tcp://{0}:{1}/mex", ipAddressDynamic, wcfTcpPortDynamic);
        
        //Create a MexTcpBinding to allow metadata to be exchanged over tcp
        Binding mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
        
        //And create the mex endpoint, exposing the IMetadataExchange contract over the mex tcp binding, on the urls we just built. 
        host.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, mexEndpointUrl, new Uri(mexListenUrl));

        //Open the host and start listening!  W00t!
        host.Open();

        //Report what URLs we are listening on...
        Trace.TraceInformation("==========\nListening On:\n==========\n\n{0}\n{1}\n{2}\n{3}\n\n==========",
                                serviceEndpointUrl, serviceListenUrl, mexEndpointUrl, mexListenUrl);

        //Loop, sleep, report, repeat
        while (true)
        {
          Thread.Sleep(30000); //Sleep for 30 seconds....
          Trace.TraceInformation("Listening...");
        }
      }
    }

    public override bool OnStart()
    {
      // Set the maximum number of concurrent connections 
      ServicePointManager.DefaultConnectionLimit = 12;

      // For information on handling configuration changes
      // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

      return base.OnStart();
    }

  }
}
