using System;
using System.Net;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Interfaces;
using AdamDotCom.Amazon.Service.Utilities;

namespace AdamDotCom.Amazon.Service
{
    public class AmazonService : IAmazon
    {
        public Reviews ReviewsXml(string customerId)
        {
            return Reviews(customerId);
        }

        public Reviews ReviewsJson(string customerId)
        {
            return Reviews(customerId);
        }

        public Wishlist WishlistXml(string listId)
        {
            return Wishlist(listId);
        }

        public Wishlist WishlistJson(string listId)
        {
            return Wishlist(listId);
        }

        public AmazonResponse DiscoverUserNameXml(string username)
        {
            throw new NotImplementedException();
        }

        public AmazonResponse DiscoverUserNameJson(string username)
        {
            throw new NotImplementedException();
        }

        private static Reviews Reviews(string customerId)
        {
            AmazonResponse amazonResponse = new AmazonFactory(BuildRequest(customerId, null)).GetResponse();

            HandleErrors(amazonResponse);

            return new Reviews(amazonResponse.Reviews);
        }

        private static Wishlist Wishlist(string listId)
        {
            AmazonResponse amazonResponse = new AmazonFactory(BuildRequest(null, listId)).GetResponse();

            HandleErrors(amazonResponse);

            return new Wishlist(amazonResponse.Products);
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

        private static void HandleErrors(IAmazonResponse amazonResponse)
        {
            if (amazonResponse.Errors != null && amazonResponse.Errors.Count != 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, amazonResponse.Errors);
            }
        }
    }
}