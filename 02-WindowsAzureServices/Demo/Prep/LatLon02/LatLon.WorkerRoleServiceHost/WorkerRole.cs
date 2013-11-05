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
using LatLon.Services;
using LatLon.Contracts;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace LatLon.WorkerRoleServiceHost
{
  public class WorkerRole : RoleEntryPoint
  {
    public override void Run()
    {
      // This is a sample worker implementation. Replace with your logic.
      Trace.TraceInformation("LatLon.WorkerRoleServiceHost entry point called");

      using (ServiceHost host = new ServiceHost(typeof(LatLonUtilitiesService)))
      {
        string ip = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["tcpinput"].IPEndpoint.Address.ToString();
        int tcpport = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["tcpinput"].IPEndpoint.Port;
        int mexport = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["mexinput"].IPEndpoint.Port;

        // Add a metadatabehavior for client proxy generation
        // The metadata is exposed via net.tcp
        ServiceMetadataBehavior metadatabehavior = new ServiceMetadataBehavior();
        host.Description.Behaviors.Add(metadatabehavior);
        Binding mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
        //string mexlistenurl = string.Format("net.tcp://{0}:{1}/MyServiceMetaDataEndpoint", ip, mexport);
        //string mexendpointurl = string.Format("net.tcp://{0}:{1}/MyServiceMetaDataEndpoint", RoleEnvironment.GetConfigurationSettingValue("Domain"), 8001);
        string mexlistenurl = string.Format("net.tcp://{0}:{1}/mex", ip, mexport);
        string mexendpointurl = string.Format("net.tcp://{0}:{1}/mex", RoleEnvironment.GetConfigurationSettingValue("Domain"), 8001);
        host.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, mexendpointurl, new Uri(mexlistenurl));

        // Add the endpoint for MyService
        //string listenurl = string.Format("net.tcp://{0}:{1}/MyServiceEndpoint", ip, tcpport);
        //string endpointurl = string.Format("net.tcp://{0}:{1}/MyServiceEndpoint", RoleEnvironment.GetConfigurationSettingValue("Domain"), 9001);
        string listenurl = string.Format("net.tcp://{0}:{1}/LatLonUtilities", ip, tcpport);
        string endpointurl = string.Format("net.tcp://{0}:{1}/LatLonUtilities", RoleEnvironment.GetConfigurationSettingValue("Domain"), 9001);
        host.AddServiceEndpoint(typeof(ILatLonUtilitiesService), new NetTcpBinding(SecurityMode.None), endpointurl, new Uri(listenurl));
        host.Open();

        Trace.TraceInformation("==========\nListening On:\n==========\n\n{0}\n{1}\n{2}\n{3}\n\n==========", endpointurl, listenurl, mexendpointurl, mexlistenurl);
       
        //Loop and sleep
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
      RoleEnvironment.Changing += RoleEnvironment_Changing;

      return base.OnStart();
    }

    private void RoleEnvironment_Changing(object sender, RoleEnvironmentChangingEventArgs e)
    {
      // If a configuration setting is changing
      if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
      {
        // Set e.Cancel to true to restart this role instance
        e.Cancel = true;
      }
    }
  }
}
