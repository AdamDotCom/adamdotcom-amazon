//This class originated from Oren Turtner's article: Signing Amazon Product Advertising API requests — the missing C# WCF sample: http://flyingpies.wordpress.com/2009/08/01/17/
// find the original source here: http://flyingpies.s3.amazonaws.com/AmazonProductAdvtApiWcfSample.zip
// Thanks Oren!

using System.ServiceModel.Channels;
using System.Xml;

namespace AdamDotCom.Amazon.WebServiceTranslator.Infrastructure
{
    public class AmazonHeader : MessageHeader
    {
        private readonly string name;
        private readonly string value;

        public AmazonHeader(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public override string Name
        {
            get { return name; }
        }

        public override string Namespace
        {
            get { return "http://security.amazonaws.com/doc/2007-01-01/"; }
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter xmlDictionaryWriter, MessageVersion messageVersion)
        {
            xmlDictionaryWriter.WriteString(value);
        }
    }
}