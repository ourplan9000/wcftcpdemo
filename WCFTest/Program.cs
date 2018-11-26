using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFContracts;

namespace WCFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var uris = new Uri[1];
            string addr = "net.tcp://192.168.0.103:9015/ProductService";
            uris[0] = new Uri(addr);


            ServiceHost host = new ServiceHost(typeof(ProductService), uris);
            var binding = new NetTcpBinding(SecurityMode.None, false)
            {
                TransferMode = TransferMode.StreamedResponse,
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647,
                ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max,
                ReceiveTimeout = new TimeSpan(0, 10, 0),
                SendTimeout = new TimeSpan(0, 10, 0),
                MaxConnections = 1000
            };
            host.AddServiceEndpoint(typeof(IProductService), binding, "");
            host.Opened += HostOpened;
            host.Open();

            Console.ReadLine();
            host.Close();
        }

        private static void HostOpened(object sender, EventArgs e)
        {
            Console.WriteLine("WCF service is started");
        }
    }
}
