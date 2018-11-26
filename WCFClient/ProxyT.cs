using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using ProtoBuf;
using WCFContracts;

namespace WCFClient
{
    public class ProxyT<TContract>
    {
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
            var binding = GeTcpBinding();
            var endPoint = new EndpointAddress(uri);
            var channel = new ChannelFactory<TContract>(binding, endPoint);
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
    }
}