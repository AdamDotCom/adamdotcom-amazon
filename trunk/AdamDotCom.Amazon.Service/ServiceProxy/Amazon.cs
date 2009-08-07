// The following line sets the default namespace for DataContract serialized typed to be ""
using System.Runtime.Serialization;
using System.ServiceModel;

[assembly: ContractNamespace("", ClrNamespace = "AmazonService")]
namespace AdamDotCom.Amazon.Service.Proxy
{
    public class AmazonService : ClientBase<IAmazon>, IAmazon
    {
        public string Greet(string name)
        {
            return base.Channel.Greet(name);
        }
    }
}
