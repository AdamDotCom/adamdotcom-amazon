using System.ServiceModel;
using System.ServiceModel.Web;

namespace AdamDotCom.Amazon.Service.Proxy
{
    [ServiceContract]
    public interface IAmazon
    {
        [OperationContract]
        [WebGet(UriTemplate = "reviews/id/{customerId}/xml")]
        Reviews ReviewsByCustomerIdXml(string customerId);

        [OperationContract]
        [WebGet(UriTemplate = "reviews/id/{customerId}/json", ResponseFormat = WebMessageFormat.Json)]
        Reviews ReviewsByCustomerIdJson(string customerId);

        [OperationContract]
        [WebGet(UriTemplate = "reviews/user/{username}/xml")]
        Reviews ReviewsByUsernameXml(string username);

        [OperationContract]
        [WebGet(UriTemplate = "reviews/user/{username}/json", ResponseFormat = WebMessageFormat.Json)]
        Reviews ReviewsByUsernameJson(string username);

        [OperationContract]
        [WebGet(UriTemplate = "wishlist/id/{listId}/xml")]
        Wishlist WishlistByListIdXml(string listId);

        [OperationContract]
        [WebGet(UriTemplate = "wishlist/id/{listId}/json", ResponseFormat = WebMessageFormat.Json)]
        Wishlist WishlistByListIdJson(string listId);

        [OperationContract]
        [WebGet(UriTemplate = "wishlist/user/{username}/xml")]
        Wishlist WishlistByUsernameXml(string username);

        [OperationContract]
        [WebGet(UriTemplate = "wishlist/user/{username}/json", ResponseFormat = WebMessageFormat.Json)]
        Wishlist WishlistByUsernameJson(string username);

        [OperationContract]
        [WebGet(UriTemplate = "discover/user/{username}/xml")]
        Profile DiscoverUsernameXml(string username);

        [OperationContract]
        [WebGet(UriTemplate = "discover/user/{username}/json", ResponseFormat = WebMessageFormat.Json)]
        Profile DiscoverUsernameJson(string username);
    }
}
