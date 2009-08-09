using System.Collections.Generic;

namespace AdamDotCom.Amazon.Domain.Interfaces
{
    public interface IProductListMapper
    {
        List<Product> GetList();
    }
}