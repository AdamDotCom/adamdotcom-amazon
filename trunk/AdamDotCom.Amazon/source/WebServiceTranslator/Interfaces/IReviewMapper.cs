using System.Collections.Generic;

namespace AdamDotCom.Amazon.WebServiceTranslator.Interfaces
{
    public interface IReviewMapper
    {
        List<ReviewDTO> GetReviews();
        
        List<string> GetErrors();
    }
}