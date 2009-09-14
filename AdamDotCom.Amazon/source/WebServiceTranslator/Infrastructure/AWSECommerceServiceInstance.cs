using System.ServiceModel;
using AdamDotCom.Amazon.WebServiceTranslator.Infrastructure;

namespace AdamDotCom.Amazon.WebServiceTranslator.Infrastructure
{
    public static class AWSECommerceServiceInstance
    {
        private static readonly AWSECommerceServicePortTypeClient awseCommerceService;

        static AWSECommerceServiceInstance()
        {
            var binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.Transport;

            awseCommerceService = new AWSECommerceServicePortTypeClient(
                binding,
                new EndpointAddress("https://ecs.amazonaws.com/onca/soap?Service=AWSECommerceService"));
        }

        public static void SetCredentials(string accessKeyId, string secretAccessKey)
        {
            if (!awseCommerceService.ChannelFactory.Endpoint.Behaviors.Contains(typeof(AmazonSigningEndpointBehavior)))
            {
                awseCommerceService.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(accessKeyId, secretAccessKey));    
            }
        }

        public static AWSECommerceServicePortTypeClient AWSECommerceService
        {
            get { return awseCommerceService; }
        }
    }
}