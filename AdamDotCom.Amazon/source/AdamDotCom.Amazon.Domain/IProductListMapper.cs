using System.Collections.Generic;

namespace AdamDotCom.Amazon.Domain
{
    public interface IProductListMapper
    {
        List<Product> GetList();
    }

}