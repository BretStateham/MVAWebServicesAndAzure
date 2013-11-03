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
using LatLon.Contracts;
using LatLon.Services;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace LatLon.WorkerRoleServiceHost
{
  public class WorkerRole : RoleEntryPoint
  {

    ServiceHost host;

    public override void Run()
    {
      while (true)
      {
        Trace.TraceInformation("Listening...");
        Thread.Sleep(30000);
      }

    }

    public override bool OnStart()
    {
      // Set the maximum number of concurrent connections 
      ServicePointManager.DefaultConnectionLimit = 12;

      // For information on handling configuration changes
      // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

      StartService();

      return base.OnStart();
    }

    private void StartService()
    {
      string ipAddress;
      int netTcpServicePort;
      int netTcpMexPort;
      string netTcpServiceUrl;
      string netTcpMexUrl;

      ipAddress = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["NetTcpServicePort"].IPEndpoint.Address.ToString();
      netTcpServicePort = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["NetTcpServicePort"].IPEndpoint.Port;
      netTcpMexPort = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["NetTcpMexPort"].IPEndpoint.Port;

      netTcpServiceUrl = string.Format("net.tcp://{0}:{1}/LatLon/LatLonUtilities", ipAddress, netTcpServicePort);
      netTcpMexUrl = string.Format("net.tcp://{0}:{1}/LatLon/mex", ipAddress, netTcpServicePort); //netTcpMexPort

      Trace.TraceInformation("netTcpServiceUrl: {0}", netTcpServiceUrl);
      Trace.TraceInformation("netTcpMexUrl:     {0}", netTcpMexUrl);

      //host = new ServiceHost(typeof(LatLonUtilitiesService), new Uri("net.tcp://localhost/LatLon"));
      host = new ServiceHost(typeof(LatLonUtilitiesService)); //No base address.  Each endpoint will specify their own base addr...

      //host.AddServiceEndpoint(
      //  typeof(ILatLonUtilitiesService),
      //  new NetTcpBinding(),
      //  "LatLonUtilities");

      //Create the service endpoint
      host.AddServiceEndpoint(typeof(ILatLonUtilitiesService), new NetTcpBinding(SecurityMode.None), netTcpServiceUrl);

      //Create the mex endpoint
      ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
      //smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
      host.Description.Behaviors.Add(smb);

      host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), netTcpMexUrl);

      //host.AddServiceEndpoint(
      //  ServiceMetadataBehavior.MexContractName,
      //  MetadataExchangeBindings.CreateMexTcpBinding(),
      //  "mex");

      host.Open();
      Trace.TraceInformation("LatLonUtilitiesService Started");

      Trace.TraceInformation("Host is listening on:");
      foreach (var addr in host.BaseAddresses)
        Trace.TraceInformation("  {0}", addr.AbsoluteUri);
    }
  }
}
