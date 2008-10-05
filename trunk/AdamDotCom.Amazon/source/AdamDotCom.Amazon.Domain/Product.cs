namespace AdamDotCom.Amazon.Domain
{
    public class Product : IProduct
    {
        public string ASIN { get; set; }

        public string Title { get; set; }

        public string Authors { get; set; }

        public string AuthorsMLA { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string ImageName { get; set; }

        public string Publisher { get; set; }

        public string ProductPreviewUrl { get; set; }
    }
}