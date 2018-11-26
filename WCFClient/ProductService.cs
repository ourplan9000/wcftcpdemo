using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using ProtoBuf;
using WCFContracts;

namespace WCFClient
{
    public class ProductService: ProxyT<IProductService>
    {
        private string uri = string.Empty;

        public ProductService(string _uri)
        {
            uri = _uri;
        }
        public async Task<List<string>> GetTaskStream()
        {
            var proxy = base.GetProxy(uri);
            try
            {
                var listStr = new List<string>();
                var result = await  proxy?.GetTaskStream();
               
                using (var stream = new MemoryStream(result))
                {
                  
                    var _fw = Serializer.Deserialize<PortoDTO>(stream);
                    Console.WriteLine(_fw.StrName);
                    listStr.AddRange(_fw.lstInfo);
                }
                base.ProxyClose(proxy);
                return listStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        public async Task<List<string>> GetTaskString()
        {
            var proxy = base.GetProxy(uri);
            try
            {
                var listStr = new List<string>();
                var result = await proxy?.GetTaskStrings();
                 
                base.ProxyClose(proxy);
                return listStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}