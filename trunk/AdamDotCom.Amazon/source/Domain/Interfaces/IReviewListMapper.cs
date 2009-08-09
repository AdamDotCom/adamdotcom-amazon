using System.Collections.Generic;

namespace AdamDotCom.Amazon.Domain.Interfaces
{
    public interface IReviewListMapper
    {
        List<Review> GetReviewList();
    }
}