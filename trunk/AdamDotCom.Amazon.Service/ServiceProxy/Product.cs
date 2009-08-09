using System.Runtime.Serialization;

namespace AdamDotCom.Amazon.Service.Proxy
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public string ASIN { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Authors { get; set; }

        [DataMember]
        public string AuthorsMLA { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public string ImageName { get; set; }

        [DataMember]
        public string Publisher { get; set; }

        [DataMember]
        public string ProductPreviewUrl { get; set; }
    }
}