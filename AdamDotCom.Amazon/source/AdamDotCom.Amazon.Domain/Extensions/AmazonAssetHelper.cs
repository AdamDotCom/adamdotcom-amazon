using AdamDotCom.Amazon.WebServiceTranslator;

namespace AdamDotCom.Amazon.Domain.Extensions
{
    public static class AmazonAssetHelper
    {
        public static string ProductPreviewUrl(this IProductDTO product, string associateTag)
        {
            return @"http://www.amazon.com/gp/product/" + product.ASIN + "?ie=UTF8&tag=" + associateTag +
                   @"&linkCode=as2&camp=1789&creative=9325&creativeASIN=" + product.ASIN;
        }

        public static string ProductImageUrl(this IProductDTO product, string associateTag)
        {
            if (associateTag == null || product.ASIN == null)
            {
                return null;
            }

            return @"http://images.amazon.com/images/P/" + product.ASIN + @".01._SCTZZZZZZZ_.jpg";
        }
    }
}