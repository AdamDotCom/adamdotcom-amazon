using System.Runtime.Serialization;
using System.ServiceModel;

[assembly: ContractNamespace("http://adam.kahtava.com/services/amazon", ClrNamespace = "AdamDotCom.Amazon.Service.Proxy")]
namespace AdamDotCom.Amazon.Service.Proxy
{
    public class AmazonService : ClientBase<IAmazon>, IAmazon
    {
        public Reviews ReviewsByCustomerIdXml(string customerId)
        {
            return base.Channel.ReviewsByCustomerIdXml(customerId);
        }

        public Reviews ReviewsByCustomerIdJson(string customerId)
        {
            return base.Channel.ReviewsByCustomerIdJson(customerId);
        }

        public Reviews ReviewsByUsernameXml(string username)
        {
            return base.Channel.ReviewsByUsernameXml(username);
        }

        public Reviews ReviewsByUsernameJson(string username)
        {
            return base.Channel.ReviewsByUsernameJson(username);
        }

        public Wishlist WishlistByListIdXml(string listId)
        {
            return base.Channel.WishlistByListIdXml(listId);
        }

        public Wishlist WishlistByListIdJson(string listId)
        {
            return base.Channel.WishlistByListIdJson(listId);
        }

        public Wishlist WishlistByUsernameXml(string username)
        {
            return base.Channel.WishlistByUsernameXml(username);
        }

        public Wishlist WishlistByUsernameJson(string username)
        {
            return base.Channel.WishlistByUsernameJson(username);
        }

        public Profile DiscoverUsernameXml(string username)
        {
            return base.Channel.DiscoverUsernameXml(username);
        }

        public Profile DiscoverUsernameJson(string username)
        {
            return base.Channel.DiscoverUsernameJson(username);
        }
    }
}