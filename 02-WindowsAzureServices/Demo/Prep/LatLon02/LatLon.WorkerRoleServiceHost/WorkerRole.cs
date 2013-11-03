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

      host = new ServiceHost(typeof(LatLonUtilitiesService), new Uri("net.tcp://localhost/LatLon"));

      host.AddServiceEndpoint(
        typeof(ILatLonUtilitiesService),
        new NetTcpBinding(),
        "LatLonUtilities");

      ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
      smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
      host.Description.Behaviors.Add(smb);

      host.AddServiceEndpoint(
        ServiceMetadataBehavior.MexContractName,
        MetadataExchangeBindings.CreateMexTcpBinding(),
        "mex");

      host.Open();
      Trace.TraceInformation("LatLonUtilitiesService Started");

      Trace.TraceInformation("Host is listening on:");
      foreach (var addr in host.BaseAddresses)
        Trace.TraceInformation("  {0}", addr.AbsoluteUri);
    }
  }
}
