using System.Collections.Generic;
using System.Runtime.Serialization;
using AdamDotCom.Amazon.Domain;

namespace AdamDotCom.Amazon.Service.Proxy
{
    [KnownType(typeof(Product))]
    [CollectionDataContract(Name = "WishList", ItemName = "Product")]
    public class Wishlist : List<Product>
    {
        public Wishlist()
        {
        }

        public Wishlist(IEnumerable<Product> wishlist) : base(wishlist)
        {
        }
    }
}