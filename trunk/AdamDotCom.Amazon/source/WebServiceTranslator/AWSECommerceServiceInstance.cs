using AdamDotCom.Amazon.WebServiceTranslator.com.amazon.webservices;

namespace AdamDotCom.Amazon.WebServiceTranslator
{
    public static class AWSECommerceServiceInstance
    {
        private static readonly AWSECommerceService awseCommerceService;
        private static AmazonHmacAssertion amazonHmacAssertion;

        static AWSECommerceServiceInstance()
        {
            awseCommerceService = new AWSECommerceService();
        }

        public static void SetPolicy(string awsAccessKeyId, string awsSecretAccessKey)
        {
            if (amazonHmacAssertion == null)
            {
                amazonHmacAssertion = new AmazonHmacAssertion(awsAccessKeyId, awsSecretAccessKey);
            }
            awseCommerceService.SetPolicy(amazonHmacAssertion.Policy());
        }

        public static AWSECommerceService AWSECommerceService
        {
            get { return awseCommerceService; }
        }
    }
}