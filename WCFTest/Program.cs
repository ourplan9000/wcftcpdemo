using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WCFContracts;

namespace WCFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ProductService)); 
            var endpoind = ProxyT<IProductService>.GetServiceEndpoint("192.168.0.103", "9015");
            host.AddServiceEndpoint(endpoind);
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
