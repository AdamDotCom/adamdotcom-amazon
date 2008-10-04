using System.Collections.Generic;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public interface IProductMapper
    {
        List<ProductDTO> GetProducts(List<string> asinList);
    }
}