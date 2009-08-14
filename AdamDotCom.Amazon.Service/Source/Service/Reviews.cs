using System.Collections.Generic;
using System.Runtime.Serialization;
using AdamDotCom.Amazon.Domain;

namespace AdamDotCom.Amazon.Service
{
    [CollectionDataContract(Name = "Reviews", ItemName = "Review")]
    public class Reviews : List<Review>
    {
        public Reviews()
        {
        }

        public Reviews(IEnumerable<Review> reviews) : base(reviews)
        {
        }
    }
}
