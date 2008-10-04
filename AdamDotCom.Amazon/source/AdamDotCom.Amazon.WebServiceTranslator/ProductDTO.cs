using AdamDotCom.Amazon.WebServiceTranslator;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public class ProductDTO : IProductDTO
    {
        public string ASIN { get; set; }

        public string Title { get; set; }

        public string[] Authors { get; set; }
        
        public string Url { get; set; }

        public string Publisher { get; set; }
    }
}