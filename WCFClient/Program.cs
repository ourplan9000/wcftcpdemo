using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFContracts;

namespace WCFClient
{
    class Program
    {
        static   void Main(string[] args)
        {

            wcfTest();
            Console.ReadLine();

        }

        static async void wcfTest()
        {

            var uri = "net.tcp://192.168.0.103:9015/ProductService";
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

            for (int i = 0; i < 20000; i++)
            {
                var endPoint = new EndpointAddress(uri);
                using (var channel = new ChannelFactory<IProductService>(binding, endPoint))
                {

                    var proxy = channel.CreateChannel();
                    for (int a = 0; a < 3; a++)
                    {
                        var result = await proxy?.GetTaskStrings();
                        result.ToList().ForEach(p => Console.WriteLine(i + " - " + p + "- - " + a));
                    }
                    channel.Close();
                    proxy = null;
                }
            }

        }
    }
}
