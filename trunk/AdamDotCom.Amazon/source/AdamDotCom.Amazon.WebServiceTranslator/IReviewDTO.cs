using System;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public interface IReviewDTO
    {
        string ASIN { get; set; }

        string Summary { get; set; }

        string Content { get; set; }

        DateTime Date { get; set; }

        int HelpfulVotes { get; set; }

        int TotalVotes { get; set; }

        decimal Rating { get; set; }
    }
}