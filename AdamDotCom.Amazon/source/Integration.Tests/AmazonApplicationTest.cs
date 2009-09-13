using AdamDotCom.Amazon.Application;
using AdamDotCom.Amazon.Application.Interfaces;
using NUnit.Framework;

namespace AdamDotCom.Amazon.UnitTests
{
    [TestFixture]
    public class AmazonApplicationTest
    {
        [Test]
        public void SanityTest()
        {
            var amazonRequest = TestHelper.ValidAmazonRequest;

            Assert.IsFalse(string.IsNullOrEmpty(amazonRequest.SecretAccessKey), "Doesn't look like the config values are being picked up!");
        }

        [Test]
        public void ShouldSerializeObject()
        {
            var amazonRequest = TestHelper.ValidAmazonRequest;

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