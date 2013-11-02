﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 12.0.21005.1
// 
namespace LatLon.Windows81Client.LatLonWcf {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://BretStateham.com/samples/2013/10/LatLon", ConfigurationName="LatLonWcf.ILatLonUtilitiesService")]
    public interface ILatLonUtilitiesService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://BretStateham.com/samples/2013/10/LatLon/ILatLonUtilitiesService/RadiansBet" +
            "weenToPoints", ReplyAction="http://BretStateham.com/samples/2013/10/LatLon/ILatLonUtilitiesService/RadiansBet" +
            "weenToPointsResponse")]
        System.Threading.Tasks.Task<double> RadiansBetweenToPointsAsync(double Latitude1, double Longitude1, double Latitude2, double Longitude2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://BretStateham.com/samples/2013/10/LatLon/ILatLonUtilitiesService/NauticalMi" +
            "lesBetweenToPoints", ReplyAction="http://BretStateham.com/samples/2013/10/LatLon/ILatLonUtilitiesService/NauticalMi" +
            "lesBetweenToPointsResponse")]
        System.Threading.Tasks.Task<double> NauticalMilesBetweenToPointsAsync(double Latitude1, double Longitude1, double Latitude2, double Longitude2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://BretStateham.com/samples/2013/10/LatLon/ILatLonUtilitiesService/Kilometers" +
            "BetweenToPoints", ReplyAction="http://BretStateham.com/samples/2013/10/LatLon/ILatLonUtilitiesService/Kilometers" +
            "BetweenToPointsResponse")]
        System.Threading.Tasks.Task<double> KilometersBetweenToPointsAsync(double Latitude1, double Longitude1, double Latitude2, double Longitude2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://BretStateham.com/samples/2013/10/LatLon/ILatLonUtilitiesService/MilesBetwe" +
            "enToPoints", ReplyAction="http://BretStateham.com/samples/2013/10/LatLon/ILatLonUtilitiesService/MilesBetwe" +
            "enToPointsResponse")]
        System.Threading.Tasks.Task<double> MilesBetweenToPointsAsync(double Latitude1, double Longitude1, double Latitude2, double Longitude2);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILatLonUtilitiesServiceChannel : LatLon.Windows81Client.LatLonWcf.ILatLonUtilitiesService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LatLonUtilitiesServiceClient : System.ServiceModel.ClientBase<LatLon.Windows81Client.LatLonWcf.ILatLonUtilitiesService>, LatLon.Windows81Client.LatLonWcf.ILatLonUtilitiesService {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public LatLonUtilitiesServiceClient() : 
                base(LatLonUtilitiesServiceClient.GetDefaultBinding(), LatLonUtilitiesServiceClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ILatLonUtilitiesService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public LatLonUtilitiesServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(LatLonUtilitiesServiceClient.GetBindingForEndpoint(endpointConfiguration), LatLonUtilitiesServiceClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public LatLonUtilitiesServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(LatLonUtilitiesServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public LatLonUtilitiesServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(LatLonUtilitiesServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public LatLonUtilitiesServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<double> RadiansBetweenToPointsAsync(double Latitude1, double Longitude1, double Latitude2, double Longitude2) {
            return base.Channel.RadiansBetweenToPointsAsync(Latitude1, Longitude1, Latitude2, Longitude2);
        }
        
        public System.Threading.Tasks.Task<double> NauticalMilesBetweenToPointsAsync(double Latitude1, double Longitude1, double Latitude2, double Longitude2) {
            return base.Channel.NauticalMilesBetweenToPointsAsync(Latitude1, Longitude1, Latitude2, Longitude2);
        }
        
        public System.Threading.Tasks.Task<double> KilometersBetweenToPointsAsync(double Latitude1, double Longitude1, double Latitude2, double Longitude2) {
            return base.Channel.KilometersBetweenToPointsAsync(Latitude1, Longitude1, Latitude2, Longitude2);
        }
        
        public System.Threading.Tasks.Task<double> MilesBetweenToPointsAsync(double Latitude1, double Longitude1, double Latitude2, double Longitude2) {
            return base.Channel.MilesBetweenToPointsAsync(Latitude1, Longitude1, Latitude2, Longitude2);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ILatLonUtilitiesService)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ILatLonUtilitiesService)) {
                return new System.ServiceModel.EndpointAddress("http://localhost/LatLonUtilitiesService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return LatLonUtilitiesServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ILatLonUtilitiesService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return LatLonUtilitiesServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ILatLonUtilitiesService);
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_ILatLonUtilitiesService,
        }
    }
}