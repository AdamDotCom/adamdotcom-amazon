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
        private readonly AmazonResponse amazonResponse;

        public AmazonFactory(IAmazonRequest amazonRequest)
        {
            amazonResponse = new AmazonResponse { Errors = new List<KeyValuePair<string, string>>() };

            amazonResponse.Errors = amazonRequest.Validate();

            if(amazonResponse.Errors.Count == 0)
            {
                if (!string.IsNullOrEmpty(amazonRequest.ListId))
                {
                    productListMapper = new ProductListMapper(amazonRequest);
                }

                if (!string.IsNullOrEmpty(amazonRequest.CustomerId))
                {
                    reviewListMapper = new ReviewListMapper(amazonRequest);
                }                
            }
        }

        public AmazonResponse GetResponse()
        {
            var amazonResponse = new AmazonResponse {Errors = new List<KeyValuePair<string, string>>()};

            if (productListMapper != null)
            {
                if (productListMapper.GetErrors().Count != 0)
                {
                    foreach (var error in productListMapper.GetErrors())
                    {
                        amazonResponse.Errors.Add(error.Translate());
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
                    foreach (var error in reviewListMapper.GetErrors())
                    {
                        amazonResponse.Errors.Add(error.Translate());
                    }
                }
                else
                {
                    amazonResponse.Reviews = reviewListMapper.GetReviewList();
                }
            }

            return amazonResponse;
        }
    }
}