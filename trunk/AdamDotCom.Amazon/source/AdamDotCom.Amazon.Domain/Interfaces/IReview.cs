using System;
using AdamDotCom.Amazon.Domain.Interfaces;

namespace AdamDotCom.Amazon.Domain.Interfaces
{
    public interface IReview : IProduct
    {
        string Summary { get; set; }

        string Content { get; set; }

        DateTime Date { get; set; }

        int HelpfulVotes { get; set; }

        int TotalVotes { get; set; }
        
        decimal Rating { get; set; }
    }
}