using System.Collections.Generic;

namespace AdamDotCom.Amazon.Domain.Extensions
{
    public static class AmazonErrorTranslator
    {
        public static KeyValuePair<string, string> Translate(this KeyValuePair<string, string> error)
        {
            if (error.Key.Contains("AWS.InvalidParameterValue"))
            {
                if (error.Value.Contains("CustomerId"))
                {
                    return new KeyValuePair<string, string>("customerId", error.Value);
                }
                if (error.Value.Contains("ListId"))
                {
                    return new KeyValuePair<string, string>("listId", error.Value);
                }
            }
            return error;
        }
    }
}