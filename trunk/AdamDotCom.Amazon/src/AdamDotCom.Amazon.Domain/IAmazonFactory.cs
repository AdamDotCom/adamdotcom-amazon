using AdamDotCom.Amazon.Domain;

namespace AdamDotCom.Amazon.Domain
{
    public interface IAmazonFactory
    {
        AmazonResponse GetResponse();
    }
}