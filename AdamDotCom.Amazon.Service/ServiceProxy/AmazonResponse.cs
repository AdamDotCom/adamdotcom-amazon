using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AdamDotCom.Amazon.Service.Proxy
{
    [DataContract]
    public class AmazonResponse
    {
        [DataMember]
        public List<Review> Reviews { get; set; }

        [DataMember]
        public List<Product> Products { get; set; }

        [DataMember]
        public List<string> Errors { get; set; }
    }
}
