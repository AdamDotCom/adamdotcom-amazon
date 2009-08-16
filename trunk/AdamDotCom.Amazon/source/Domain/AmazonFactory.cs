using System.Collections.Generic;
using System.Runtime.Serialization;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Extensions;
using AdamDotCom.Amazon.Domain.Interfaces;

[assembly: ContractNamespace("http://adam.kahtava.com/services/amazon", ClrNamespace = "AdamDotCom.Amazon.Domain")]
namespace AdamDotCom.Amazon.Domain
{
    public class AmazonFactory : IAmazonFactory
    {
        private readonly ProductListMapper productListMapper;
        private readonly ReviewListMapper reviewListMapper;
        private readonly IAmazonRequest amazonRequest;

        public AmazonFactory(IAmazonRequest amazonRequest)
        {
            this.amazonRequest = amazonRequest;

            if (!string.IsNullOrEmpty(amazonRequest.ListId))
            {
                productListMapper = new ProductListMapper(amazonRequest);
            }

            if (!string.IsNullOrEmpty(amazonRequest.CustomerId))
            {
                reviewListMapper = new ReviewListMapper(amazonRequest);
            }
        }

        public AmazonResponse GetResponse()
        {
            var amazonResponse = new AmazonResponse {Errors = new List<KeyValuePair<string, string>>()};

            Validate(amazonResponse);

            if (productListMapper != null)
            {
                if (productListMapper.GetErrors().Count != 0)
                {
                    foreach (var item in productListMapper.GetErrors())
                    {
                        amazonResponse.Errors.Add(item.Translate());
                    }
                }
                else
                {
                    amazonResponse.Products = productListMapper.GetList();
                }
            }

            if (reviewListMapper != null)
            {
                if (reviewListMapper.GetErrors().Count != 0)
                {
                    foreach (var item in reviewListMapper.GetErrors())
                    {
                        amazonResponse.Errors.Add(item.Translate());
                    }
                }
                else
                {
                    amazonResponse.Reviews = reviewListMapper.GetReviewList();
                }
            }

            return amazonResponse;
        }

        private void Validate(AmazonResponse amazonResponse)
        {
            if (amazonRequest.CustomerId == null || amazonRequest.ListId == null)
            {
                if (amazonRequest.ListId == null && string.IsNullOrEmpty(amazonRequest.CustomerId))
                {
                    amazonResponse.Errors.Add(new KeyValuePair<string, string>("CustomerId", "A CustomerId must be specified."));
                }
                if (amazonRequest.CustomerId == null && string.IsNullOrEmpty(amazonRequest.ListId))
                {
                    amazonResponse.Errors.Add(new KeyValuePair<string, string>("ListId", "A ListId must be specified."));
                }
            }
            else if (string.IsNullOrEmpty(amazonRequest.CustomerId) || string.IsNullOrEmpty(amazonRequest.ListId))
            {
                if (string.IsNullOrEmpty(amazonRequest.CustomerId))
                {
                    amazonResponse.Errors.Add(new KeyValuePair<string, string>("CustomerId", "A CustomerId must be specified."));
                }
                if (string.IsNullOrEmpty(amazonRequest.ListId))
                {
                    amazonResponse.Errors.Add(new KeyValuePair<string, string>("ListId", "A ListId must be specified."));
                }
            }
        }
    }
}