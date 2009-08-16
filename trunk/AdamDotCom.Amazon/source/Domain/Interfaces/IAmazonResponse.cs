using System.Collections.Generic;

namespace AdamDotCom.Amazon.Domain.Interfaces
{
    public interface IAmazonResponse
    {
        List<Review> Reviews { get; set; }
        List<Product> Products { get; set; }
        List<KeyValuePair<string, string>> Errors { get; set; }
    }
}