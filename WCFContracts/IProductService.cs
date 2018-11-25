using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFContracts
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        string[] GetStrings();


        [OperationContract]
        Task<string[]> GetTaskStrings();
    }
}
