namespace AdamDotCom.Amazon.Domain.Interfaces
{
    public interface IProduct
    {
        string ASIN { get; set; }

        string Title { get; set; }

        string Authors { get; set; }

        string AuthorsMLA { get; set; }

        string Url { get; set; }

        string ImageUrl { get; set; }

        string Publisher { get; set; }

        string ProductPreviewUrl { get; set; }
    }
}