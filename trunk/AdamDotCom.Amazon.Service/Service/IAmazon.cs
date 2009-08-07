using System.ServiceModel;
using System.ServiceModel.Web;

namespace AdamDotCom.Amazon.Service
{
    [ServiceContract]
    public interface IAmazon
    {
        [OperationContract]
        [WebGet(UriTemplate = "greet/{name}")]
        string Greet(string name);
    }
}