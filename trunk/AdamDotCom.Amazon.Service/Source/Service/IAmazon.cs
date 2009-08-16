using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

[assembly: ContractNamespace("http://adam.kahtava.com/services/amazon", ClrNamespace = "AdamDotCom.Amazon.Service")]
namespace AdamDotCom.Amazon.Service
{
    [ServiceContract(Namespace = "http://adam.kahtava.com/services/amazon")]
    public interface IAmazon
    {
        [OperationContract]
        [WebGet(UriTemplate = "reviews/{CustomerId}/xml")]
        Reviews ReviewsXml(string CustomerId);

        [OperationContract]
        [WebGet(UriTemplate = "reviews/{CustomerId}/json", ResponseFormat = WebMessageFormat.Json)]
        Reviews ReviewsJson(string CustomerId);

        [OperationContract]
        [WebGet(UriTemplate = "wishlist/{ListId}/xml")]
        Wishlist WishlistXml(string ListId);

        [OperationContract]
        [WebGet(UriTemplate = "wishlist/{ListId}/json", ResponseFormat = WebMessageFormat.Json)]
        Wishlist WishlistJson(string ListId);

        [OperationContract]
        [WebGet(UriTemplate = "discover/{Username}/xml")]
        Profile DiscoverUserXml(string Username);

        [OperationContract]
        [WebGet(UriTemplate = "discover/{Username}/json", ResponseFormat = WebMessageFormat.Json)]
        Profile DiscoverUserJson(string Username);
    }
}