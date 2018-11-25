using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
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

            
                var endPoint = new EndpointAddress(uri);
                using (var channel = new ChannelFactory<IProductService>(binding, endPoint))
                {

                    var proxy = channel.CreateChannel();
                    for (int a = 0; a < 3; a++)
                    {
                        var result = await proxy?.GetTaskStrings();
                        result.ToList().ForEach(p => Console.WriteLine(a + " - " + p + "- - " + a));
                    }
                    for (int a = 0; a < 3; a++)
                    {
                        var result = await proxy?.GetTaskStream();
                        using (var stream = new MemoryStream(result))
                        {
                            var _fw = Serializer.Deserialize<PortoDTO>(stream);
                            Console.WriteLine(_fw.StrName);
                        }
                    }

                    channel.Close();
                    proxy = null;
                }

        }
    }
}
