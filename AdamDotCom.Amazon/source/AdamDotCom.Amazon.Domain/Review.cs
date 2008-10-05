using System;

namespace AdamDotCom.Amazon.Domain
{
    public class Review : IReview
    {
        public string Summary  { get; set; }

        public string Content  { get; set; }

        public DateTime Date  { get; set; }

        public int HelpfulVotes  { get; set; }

        public int TotalVotes  { get; set; }

        public decimal Rating  { get; set; }

        public string ASIN  { get; set; }

        public string Title  { get; set; }

        public string AuthorsMLA  { get; set; }

        public string Authors  { get; set; }

        public string Url  { get; set; }

        public string ImageUrl  { get; set; }

        public string Publisher  { get; set; }

        public string ProductPreviewUrl  { get; set; }
    }
}