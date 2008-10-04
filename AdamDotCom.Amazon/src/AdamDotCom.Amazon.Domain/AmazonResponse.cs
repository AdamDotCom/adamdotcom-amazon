using System.Collections.Generic;

namespace AdamDotCom.Amazon.Domain
{
    public class AmazonResponse : IAmazonResponse
    {
        public List<Review> Reviews { get; set; }

        public List<Product> Products { get; set; }

        public List<string> Errors { get; set; }
    }
}