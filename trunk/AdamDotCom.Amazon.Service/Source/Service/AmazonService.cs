using System.Linq;
using System.Net;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Interfaces;
using AdamDotCom.Amazon.Service.Extensions;
using AdamDotCom.Amazon.Service.Utilities;

namespace AdamDotCom.Amazon.Service
{
    public class AmazonService : IAmazon
    {
        public Reviews ReviewsXml(string CustomerId)
        {
            return Reviews(CustomerId);
        }

        public Reviews ReviewsJson(string customerId)
        {
            return Reviews(customerId);
        }

        public Wishlist WishlistXml(string ListId)
        {
            return Wishlist(ListId);
        }

        public Wishlist WishlistJson(string ListId)
        {
            return Wishlist(ListId);
        }

        public Profile DiscoverUserXml(string Username)
        {
            return DiscoverUser(Username);
        }

        public Profile DiscoverUserJson(string Username)
        {
            return DiscoverUser(Username);
        }

        private static Reviews Reviews(string CustomerId)
        {
            if(ServiceCache.IsInCache(CustomerId))
            {
                return (Reviews) ServiceCache.GetFromCache(CustomerId);
            }
            
            var amazonResponse = new AmazonFactory(BuildRequest(CustomerId, null)).GetResponse();

            HandleErrors(amazonResponse);

            return new Reviews(amazonResponse.Reviews.OrderByDescending(r => r.Date)).AddToCache(CustomerId);
        }

        private static Wishlist Wishlist(string ListId)
        {
            if(ServiceCache.IsInCache(ListId))
            {
                return (Wishlist) ServiceCache.GetFromCache(ListId);
            }

            var amazonResponse = new AmazonFactory(BuildRequest(null, ListId)).GetResponse();

            HandleErrors(amazonResponse);

            return new Wishlist(amazonResponse.Products.OrderBy(p => p.AuthorsMLA).ThenBy(p => p.Title)).AddToCache(ListId);
        }

        private static Profile DiscoverUser(string Username)
        {
            if (ServiceCache.IsInCache(Username))
            {
                return (Profile) ServiceCache.GetFromCache(Username);
            }

            var sniffer = new ProfileSniffer(Username);

            var customerId = sniffer.GetCustomerId();
            var listId = sniffer.GetListId();
            
            HandleErrors(sniffer);

            return new Profile {CustomerId = customerId, ListId = listId}.AddToCache(Username);
        }

        private static AmazonRequest BuildRequest(string customerId, string listId)
        {
            return new AmazonRequest
            {
                AssociateTag = "adamkahtavaap-20",
                AccessKeyId = "1MRFMGASE6CQKS2WTMR2",
                CustomerId = customerId,
                ListId = listId,
                SecretAccessKey = "XQDk151teVewB/F2wKQkUEb98aIzZYE1sA/lCrt0"
            };
        }

        private static void HandleErrors(IAmazonResponse amazonResponse)
        {
            if (amazonResponse.Errors != null && amazonResponse.Errors.Count != 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, amazonResponse.Errors, (int) ErrorCode.InternalError);
            }
        }

        private static void HandleErrors(ProfileSniffer sniffer)
        {
            if(sniffer.Errors != null && sniffer.Errors.Count != 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, sniffer.Errors, (int)ErrorCode.InternalError);
            }
        }
    }
}