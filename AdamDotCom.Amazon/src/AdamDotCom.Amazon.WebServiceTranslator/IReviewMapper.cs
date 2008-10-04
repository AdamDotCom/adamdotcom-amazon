using System.Collections.Generic;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public interface IReviewMapper
    {
        List<ReviewDTO> GetReviews();
    }
}