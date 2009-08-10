using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using AdamDotCom.Amazon.Domain;

[assembly: ContractNamespace("http://adam.kahtava.com/services/amazon", ClrNamespace = "AdamDotCom.Amazon.Service")]
namespace AdamDotCom.Amazon.Service
{
    [ServiceContract(Namespace = "http://adam.kahtava.com/services/amazon")]
    public interface IAmazon
    {
        [OperationContract]
        [WebGet(UriTemplate = "reviews/{customerId}/xml")]
        AmazonResponse Reviews(string customerId);

        [OperationContract]
        [WebGet(UriTemplate = "wishlist/{listId}/xml")]
        AmazonResponse Wishlist(string listId);
    }
}