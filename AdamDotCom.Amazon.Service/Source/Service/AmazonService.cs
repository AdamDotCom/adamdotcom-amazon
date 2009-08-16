using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Interfaces;
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
            AmazonResponse amazonResponse = new AmazonFactory(BuildRequest(CustomerId, null)).GetResponse();

            HandleErrors(amazonResponse);

            return new Reviews(amazonResponse.Reviews.OrderByDescending(r => r.Date));
        }

        private static Wishlist Wishlist(string ListId)
        {
            AmazonResponse amazonResponse = new AmazonFactory(BuildRequest(null, ListId)).GetResponse();

            HandleErrors(amazonResponse);

            return new Wishlist(amazonResponse.Products.OrderBy(p => p.AuthorsMLA).ThenBy(p => p.Title));
        }

        private static AmazonRequest BuildRequest(string customerId, string listId)
        {
            return new AmazonRequest
                       {
                           AssociateTag = "adamkahtavaap-20",
                           AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2",
                           CustomerId = customerId,
                           ListId = listId
                       };
        }

        private static Profile DiscoverUser(string Username)
        {
            var sniffer = new ProfileSniffer(Username);

            var customerId = sniffer.GetCustomerId();
            var listId = sniffer.GetListId();
            
            HandleErrors(sniffer);

            return new Profile {CustomerId = customerId, ListId = listId};
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