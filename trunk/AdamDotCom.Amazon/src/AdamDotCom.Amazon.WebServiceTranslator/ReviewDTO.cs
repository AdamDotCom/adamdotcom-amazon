using System;
using AdamDotCom.Amazon.WebServiceTranslator;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public class ReviewDTO : IReviewDTO
    {
        public string ASIN { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int HelpfulVotes { get; set; }

        public int TotalVotes { get; set; }

        public decimal Rating { get; set; }
    }
}