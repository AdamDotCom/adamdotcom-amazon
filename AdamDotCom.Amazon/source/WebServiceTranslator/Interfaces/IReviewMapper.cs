using System.Collections.Generic;

namespace AdamDotCom.Amazon.WebServiceTranslator.Interfaces
{
    public interface IReviewMapper
    {
        List<ReviewDTO> GetReviews();
        
        List<KeyValuePair<string, string>> GetErrors();
    }
}