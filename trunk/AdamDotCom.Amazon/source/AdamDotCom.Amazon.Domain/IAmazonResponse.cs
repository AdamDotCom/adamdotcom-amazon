using System.Collections.Generic;

namespace AdamDotCom.Amazon.Domain
{
    public interface IAmazonResponse
    {
        List<Review> Reviews { get; set; }
        List<Product> Products { get; set; }
        List<string> Errors { get; set; }
    }
}