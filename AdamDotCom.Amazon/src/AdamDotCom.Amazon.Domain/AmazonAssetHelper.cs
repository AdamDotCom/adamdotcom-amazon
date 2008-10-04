namespace AdamDotCom.Amazon.Domain
{
    public static class AmazonAssetHelper
    {
        public static string ProductPreviewUrl(string associateTag, string asin)
        {
            return @"http://www.amazon.com/gp/product/" + asin + "?ie=UTF8&tag=" + associateTag +
                   @"&linkCode=as2&camp=1789&creative=9325&creativeASIN=" + asin;
        }

        public static string ProductImageUrl(string associateTag, string asin)
        {
            if(associateTag == null || asin == null)
            {
                return null;
            }

            return @"http://images.amazon.com/images/P/" + asin + @".01._SCTZZZZZZZ_.jpg";
        }
    }
}