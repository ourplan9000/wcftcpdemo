using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using ProtoBuf;
using WCFContracts;

namespace WCFClient
{
    public class ProductSer
    {

        static async void wcfTest()
        {

            var uri = "net.tcp://192.168.0.103:9015/ProductService";

            var list = await gerResult(uri);
            list.ForEach(p => Console.WriteLine(p));
            for (int i = 0; i < 10000; i++)
            {
                var j = i;
                await Task.Run(async () =>
                {
                    var list1 = await gerStrResult(uri);
                    list1.ForEach(p => Console.WriteLine(p));
                    Console.WriteLine(j);
                });
            }
        }

        static async Task<List<string>> gerResult(string uri)
        {
            var proxy = GetProxy(uri);
            try
            {
                var result = await proxy?.GetTaskStrings();
                return result.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                var communicationObject = proxy as ICommunicationObject;
                if (communicationObject != null) communicationObject.Close();
            }
        }
        static async Task<List<string>> gerStrResult(string uri)
        {
            var proxy = GetProxy(uri);
            try
            {
                var listStr = new List<string>();
                var result = await proxy?.GetTaskStream();
                using (var stream = new MemoryStream(result))
                {
                    var _fw = Serializer.Deserialize<PortoDTO>(stream);
                    Console.WriteLine(_fw.StrName);
                    listStr.AddRange(_fw.lstInfo);
                }
                return listStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                var communicationObject = proxy as ICommunicationObject;
                if (communicationObject != null) communicationObject.Close();
            }

        }

        static IProductService GetProxy(string uri)
        {
            var binding = GeTcpBinding();
            var endPoint = new EndpointAddress(uri);
            var channel = new ChannelFactory<IProductService>(binding, endPoint);
            var proxy = channel.CreateChannel();
            return proxy;
        }

        static NetTcpBinding GeTcpBinding()
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