using System.Collections.Generic;
using AdamDotCom.Amazon.Domain.Interfaces;

namespace AdamDotCom.Amazon.Domain
{
    public class AmazonResponse : IAmazonResponse
    {
        public List<Review> Reviews { get; set; }
        public List<Product> Products { get; set; }
        public List<KeyValuePair<string, string>> Errors { get; set; }
    }
}