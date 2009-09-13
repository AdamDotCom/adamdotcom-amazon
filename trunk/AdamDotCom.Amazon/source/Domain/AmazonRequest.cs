using AdamDotCom.Amazon.Domain.Interfaces;

namespace AdamDotCom.Amazon.Domain
{
    public class AmazonRequest : IAmazonRequest
    {
        /// <summary>
        /// Your Amazon Web Service (AWS) access key.
        /// Dont' have one? Get one here: http://aws.amazon.com/
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// Your Amazon Associates tag.
        /// Dont' have one? Get one here: https://affiliate-program.amazon.com/gp/associates/join
        /// </summary>
        public string AssociateTag { get; set; }

        /// <summary>
        /// The listid you want to retrieve a list from.
        /// Don't know where to find this?
        /// I found my ListId by navigating to my wishlist: http://www.amazon.com/gp/registry/registry.html/?id=3JU6ASKNUS7B8
        /// then pulling out 3JU6ASKNUS7B8 (the identifier afer id= from the url above).
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// The CustomerId you want to retrieve reviews from.
        /// Don't know where to find this?
        /// I found my CustomerId by navigating to my reviews: http://www.amazon.com/gp/cdp/member-reviews/A2JM0EQJELFL69/
        /// then pulling out A2JM0EQJELFL69 (the last element of the url above).
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Your Amazon Subscription Id.
        /// Don't have one? Get one here: https://aws-portal.amazon.com/gp/aws/developer/account/index.html
        /// </summary>
        public string SecretAccessKey { get; set; }
    }
}