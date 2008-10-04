using AdamDotCom.Amazon.Domain;

namespace AdamDotCom.Amazon.Domain
{
    public class AmazonFactory : IAmazonFactory
    {
        private readonly ProductListMapper productListMapper;
        private readonly ReviewListMapper reviewListMapper;

        public AmazonFactory(IAmazonRequest amazonRequest)
        {
            productListMapper = new ProductListMapper(amazonRequest);

            reviewListMapper = new ReviewListMapper(amazonRequest);
        }

        public AmazonResponse GetResponse()
        {
            var amazonResponse = new AmazonResponse();

            amazonResponse.Products = productListMapper.GetList();
            amazonResponse.Errors = productListMapper.GetErrors();

            amazonResponse.Reviews = reviewListMapper.GetReviewList();
            
            foreach (string error in reviewListMapper.GetErrors())
            {
                amazonResponse.Errors.Add(error);
            }

            return amazonResponse;
        }
    }
}