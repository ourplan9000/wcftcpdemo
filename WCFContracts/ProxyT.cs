using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using System.Xml;
using ProtoBuf;
using WCFContracts;

namespace WCFContracts
{
    public class ProxyT<TContract>
    {
        readonly string ServerIp=String.Empty;
        readonly string ServerPort = String.Empty;

        public ProxyT(string _ServerIp, string _ServerPort)
        {
            ServerIp = _ServerIp;

            ServerPort = _ServerPort;

        }
        public virtual void ProxyClose(TContract proxy)
        {
            
            try
            {
                if (proxy != null)
                {
                    var communicationObject = proxy as ICommunicationObject;
                    if (communicationObject != null) communicationObject.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 

        }

        protected TContract GetProxy(string uri)
        {
          
            var channel = new ChannelFactory<TContract>(GetServiceEndpoint(ServerIp, ServerPort));
            var proxy = channel.CreateChannel();
            return proxy;
        }

        private NetTcpBinding GeTcpBinding()
        {
            return new NetTcpBinding(SecurityMode.None, false)
            {
                TransferMode = TransferMode.StreamedResponse,
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
                ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max,
                ReceiveTimeout = new TimeSpan(0, 10, 0),
                SendTimeout = new TimeSpan(0, 10, 0)
            };
        }
        public static ServiceEndpoint GetServiceEndpoint(string serverIp,string serverPort)
        {
            if (string.IsNullOrEmpty(serverIp) || string.IsNullOrEmpty(serverPort))
                return null;

            var netBind = new NetTcpBinding(SecurityMode.None)
            {
                TransferMode = TransferMode.StreamedResponse,
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
                ReaderQuotas = XmlDictionaryReaderQuotas.Max,
                ReceiveTimeout = new TimeSpan(0, 10, 0),
                SendTimeout = new TimeSpan(0, 10, 0),
                MaxConnections = 1000
            };

            netBind.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;

            return new ServiceEndpoint(ContractDescription.GetContract(typeof(TContract)),
                netBind,
                new EndpointAddress(String.Format("net.tcp://{0}:{1}/services/{2}", serverIp, serverPort,
                    typeof(TContract).Name.Substring(1).ToLower())));
        }
    }
}