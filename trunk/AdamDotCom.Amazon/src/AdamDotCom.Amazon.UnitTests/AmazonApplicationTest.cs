using AdamDotCom.Amazon.Application;
using AdamDotCom.Amazon.Domain;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class AmazonApplicationTest
    {
        [Test]
        public void ShouldSerializeObject()
        {
            IAmazonRequest amazonRequest = new AmazonRequest();
            amazonRequest.AssociateTag = "adamkahtavaap-20";
            amazonRequest.AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2";
            amazonRequest.CustomerId = "A2JM0EQJELFL69";
            amazonRequest.ListId = "3JU6ASKNUS7B8";

            IFileParameters fileParameters = new FileParameters();
            fileParameters.ProductFileNameAndPath = @"../../../../Products.xml";
            fileParameters.ReviewFileNameAndPath = @"../../../../Reviews.xml";
            fileParameters.ErrorFileNameAndPath = @"../../../../Errors.xml";

            IAmazonApplication amazonXmlFactory = new AmazonApplication(amazonRequest, fileParameters);
            
            Assert.AreEqual(true, amazonXmlFactory.Save());
        }
    }
}