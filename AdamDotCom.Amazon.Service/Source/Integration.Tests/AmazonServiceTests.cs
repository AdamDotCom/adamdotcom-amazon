using NUnit.Framework;
using AdamDotCom.Amazon.Service.Proxy;

namespace AdamDotCom.Amazon.Service.Integration.Tests
{
    [TestFixture]
    public class AmazonServiceTests
    {
        [Test]
        public void ShouldVerifyProxyAndReturnReviews()
        {
            var amazonService = new AmazonService();

            var response = amazonService.Reviews("A2JM0EQJELFL69");

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Reviews);
            Assert.Greater(response.Reviews.Count, 1);
        }

        [Test]
        public void ShouldVerifyProxyAndReturnWishlist()
        {
            var amazonService = new AmazonService();

            var response = amazonService.Wishlist("3JU6ASKNUS7B8");

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Products);
            Assert.Greater(response.Products.Count, 1);
        }

    }
}
