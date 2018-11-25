using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using WCFContracts;

namespace WCFTest
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ProductService : IProductService
    {
        public string[] GetStrings()
        {
            return new string[] { "Produto1", "Produto2" };
        }

        public Task<string[]> GetTaskStrings()
        {
            return Task.Run(() => new string[]
            {
                "Produto1", Guid.NewGuid().ToString("N")
            });
        }

        public Task<byte[]> GetTaskStream()
        {
            return Task.Run(() =>
            {
                PortoDTO fw = new PortoDTO();
                fw.Number = 123456;
                fw.StrName = "WCFTest";
                fw.lstInfo= new List<string>()
                {
                    "1",
                   "2",
                    "3"
                };
                byte[] data;
                using (var stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, fw);
                    data = stream.ToArray();
                }
                return data;
            });
        }
    }
}
