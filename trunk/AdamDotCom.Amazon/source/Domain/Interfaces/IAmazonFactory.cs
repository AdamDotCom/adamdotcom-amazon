using AdamDotCom.Amazon.Domain;

namespace AdamDotCom.Amazon.Domain.Interfaces
{
    public interface IAmazonFactory
    {
        AmazonResponse GetResponse();
    }
}