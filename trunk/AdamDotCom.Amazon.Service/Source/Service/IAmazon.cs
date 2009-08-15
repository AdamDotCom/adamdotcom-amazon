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
        Reviews ReviewsXml(string customerId);

        [OperationContract]
        [WebGet(UriTemplate = "reviews/{customerId}/json", ResponseFormat = WebMessageFormat.Json)]
        Reviews ReviewsJson(string customerId);

        [OperationContract]
        [WebGet(UriTemplate = "wishlist/{listId}/xml")]
        Wishlist WishlistXml(string listId);

        [OperationContract]
        [WebGet(UriTemplate = "wishlist/{listId}/json", ResponseFormat = WebMessageFormat.Json)]
        Wishlist WishlistJson(string listId);

        [OperationContract]
        [WebGet(UriTemplate = "discover/{username}/xml")]
        AmazonResponse DiscoverUserXml(string username);

        [OperationContract]
        [WebGet(UriTemplate = "discover/{username}/json", ResponseFormat = WebMessageFormat.Json)]
        AmazonResponse DiscoverUserJson(string username);
    }
}