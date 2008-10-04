namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public interface IProductDTO
    {
        string ASIN { get; set; }

        string Title { get; set; }

        string[] Authors { get; set; }

        string Url { get; set; }

        string Publisher { get; set; }
    }
}