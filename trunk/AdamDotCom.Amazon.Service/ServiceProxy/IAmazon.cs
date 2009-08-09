using System.ServiceModel;
using System.ServiceModel.Web;

namespace AdamDotCom.Amazon.Service.Proxy
{
    [ServiceContract]
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
