using System.Collections.Generic;

namespace AdamDotCom.Amazon.WebServiceTranslator.Interfaces
{
    public interface IProductMapper
    {
        List<ProductDTO> GetProducts(List<string> asinList);
        List<string> GetErrors();
    }
}