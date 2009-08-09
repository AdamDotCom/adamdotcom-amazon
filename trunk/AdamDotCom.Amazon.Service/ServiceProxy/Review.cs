using System;
using System.Runtime.Serialization;

namespace AdamDotCom.Amazon.Service.Proxy
{
    [DataContract]
    public class Review
    {
        [DataMember]
        public string Summary { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int HelpfulVotes { get; set; }

        [DataMember]
        public int TotalVotes { get; set; }

        [DataMember]
        public decimal Rating { get; set; }

        [DataMember]
        public string ASIN { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string AuthorsMLA { get; set; }

        [DataMember]
        public string Authors { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public string Publisher { get; set; }

        [DataMember]
        public string ProductPreviewUrl { get; set; }
    }
}
