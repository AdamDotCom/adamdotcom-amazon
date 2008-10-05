using System.Collections.Generic;
using AdamDotCom.Amazon.Domain;

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

                foreach (string error in productListMapper.GetErrors())
                {
                    amazonResponse.Errors.Add(error);
                }
            }

            if (reviewListMapper != null)
            {
                amazonResponse.Reviews = reviewListMapper.GetReviewList();

                foreach (string error in reviewListMapper.GetErrors())
                {
                    amazonResponse.Errors.Add(error);
                }
            }

            return amazonResponse;
        }
    }
}