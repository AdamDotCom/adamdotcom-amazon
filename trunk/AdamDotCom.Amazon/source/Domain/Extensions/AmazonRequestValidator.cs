using System.Collections.Generic;
using AdamDotCom.Amazon.Domain.Interfaces;

namespace AdamDotCom.Amazon.Domain.Extensions
{
    public static class AmazonRequestValidator
    {
        public static List<KeyValuePair<string, string>> Validate(this IAmazonRequest amazonRequest)
        {
            var errors = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrEmpty(amazonRequest.SecretAccessKey))
            {
                errors.Add(new KeyValuePair<string, string>("SecretAccessKey", "Your SecretAccessKey must be specified."));
            }

            if (string.IsNullOrEmpty(amazonRequest.AccessKeyId))
            {
                errors.Add(new KeyValuePair<string, string>("AWSAccessKeyId", "Your AccessKeyId must be specified."));
            }

            if (amazonRequest.CustomerId == null || amazonRequest.ListId == null)
            {
                if (amazonRequest.ListId == null && string.IsNullOrEmpty(amazonRequest.CustomerId))
                {
                    errors.Add(new KeyValuePair<string, string>("CustomerId", "A CustomerId must be specified."));
                }
                if (amazonRequest.CustomerId == null && string.IsNullOrEmpty(amazonRequest.ListId))
                {
                    errors.Add(new KeyValuePair<string, string>("ListId", "A ListId must be specified."));
                }
            }
            else if (string.IsNullOrEmpty(amazonRequest.CustomerId) || string.IsNullOrEmpty(amazonRequest.ListId))
            {
                if (string.IsNullOrEmpty(amazonRequest.CustomerId))
                {
                    errors.Add(new KeyValuePair<string, string>("CustomerId", "A CustomerId must be specified."));
                }
                if (string.IsNullOrEmpty(amazonRequest.ListId))
                {
                    errors.Add(new KeyValuePair<string, string>("ListId", "A ListId must be specified."));
                }
            }

            return errors;
        }
    }
}
