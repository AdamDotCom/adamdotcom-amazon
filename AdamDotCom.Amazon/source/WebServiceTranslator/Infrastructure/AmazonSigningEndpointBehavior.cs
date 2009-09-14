//This class originated from Oren Turtner's article: Signing Amazon Product Advertising API requests — the missing C# WCF sample: http://flyingpies.wordpress.com/2009/08/01/17/
// find the original source here: http://flyingpies.s3.amazonaws.com/AmazonProductAdvtApiWcfSample.zip
// Thanks Oren!

using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace AdamDotCom.Amazon.WebServiceTranslator.Infrastructure
{
    public class AmazonSigningEndpointBehavior : IEndpointBehavior
    {
        private readonly string accessKeyId;
        private readonly string secretKey;

        public AmazonSigningEndpointBehavior(string accessKeyId, string secretKey)
        {
            this.accessKeyId = accessKeyId;
            this.secretKey = secretKey;
        }

        #region IEndpointBehavior Members

        public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new AmazonSigningMessageInspector(accessKeyId, secretKey));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
        {
            return;
        }

        public void Validate(ServiceEndpoint serviceEndpoint)
        {
            return;
        }

        public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        #endregion
    }
}