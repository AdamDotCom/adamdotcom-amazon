using System.Collections.Generic;

namespace AdamDotCom.Amazon.Domain
{
    public interface IReviewListMapper
    {
        List<Review> GetReviewList();
    }
}