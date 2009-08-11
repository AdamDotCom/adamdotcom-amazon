using AdamDotCom.Amazon.Application;
using AdamDotCom.Amazon.Application.Interfaces;
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
            var amazonRequest = new AmazonRequest
                                    {
                                        AssociateTag = "adamkahtavaap-20",
                                        AWSAccessKeyId = "1MRFMGASE6CQKS2WTMR2",
                                        CustomerId = "A2JM0EQJELFL69",
                                        ListId = "3JU6ASKNUS7B8"
                                    };

            var fileParameters = new FileParameters
                                     {
                                         ProductFileNameAndPath = @"Products.xml",
                                         ReviewFileNameAndPath = @"Reviews.xml",
                                         ErrorFileNameAndPath = @"Errors.xml"
                                     };

            IAmazonApplication amazonApplication = new AmazonApplication(amazonRequest, fileParameters);
            
            Assert.AreEqual(true, amazonApplication.Save());
        }
    }
}