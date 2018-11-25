using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
    }
}
