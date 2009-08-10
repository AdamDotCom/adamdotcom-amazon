using AdamDotCom.Amazon.Domain;

namespace AdamDotCom.Amazon.Service
{
    public class AmazonService : IAmazon
    {
        public AmazonResponse Reviews(string customerId)
        {
            return new AmazonFactory(BuildRequest(customerId, null)).GetResponse();
        }

        public AmazonResponse Wishlist(string listId)
        {
            return new AmazonFactory(BuildRequest(null, listId)).GetResponse();
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
    }
}