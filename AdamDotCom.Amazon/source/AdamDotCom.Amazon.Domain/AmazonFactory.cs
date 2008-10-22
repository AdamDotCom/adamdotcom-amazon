using System.Collections.Generic;
using AdamDotCom.Amazon.Domain;
using AdamDotCom.Amazon.Domain.Interfaces;

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
            var amazonResponse = new AmazonResponse();
            amazonResponse.Errors = new List<string>();

            if(string.IsNullOrEmpty(amazonRequest.CustomerId) && string.IsNullOrEmpty(amazonRequest.ListId))
            {
                amazonResponse.Errors.Add("A CustomerId or a ListId must be specified.");
            }

            if (productListMapper != null)
            {
                amazonResponse.Products = productListMapper.GetList();

                amazonResponse.Errors.AddRange(productListMapper.GetErrors());
            }

            if (reviewListMapper != null)
            {
                amazonResponse.Reviews = reviewListMapper.GetReviewList();

                amazonResponse.Errors.AddRange(reviewListMapper.GetErrors());
            }

            return amazonResponse;
        }
    }
}