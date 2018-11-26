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
        static void Main(string[] args)
        {

            Testwcf();
            Console.ReadLine();

        }

        static async void Testwcf()
        {
            var uri = "net.tcp://192.168.0.103:9015/ProductService";
            ProductService productService = new ProductService(uri);
            var list = await productService.GetTaskString();
            list.ForEach(p => Console.WriteLine(p));
            for (int i = 0; i < 10000; i++)
            {
                var j = i;
                await Task.Run(async () =>
                {
                    var list1 = await productService.GetTaskStream();
                    list1.ForEach(p => Console.WriteLine(p));
                    Console.WriteLine(j);
                });
            }
        }


    }
}
