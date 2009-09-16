using System;
using NUnit.Framework;
using AdamDotCom.Amazon.Service.Proxy;

namespace AdamDotCom.Amazon.Service.Integration.Tests
{
    [TestFixture]
    public class AmazonServiceTests
    {
        [Test]
        //ToDo: Start WebDev.WebServer.exe before the tests are run in TestSetup then delete this test
        public void SanityTest()
        {
            var amazonService = new AmazonService();

            try
            {
                amazonService.DiscoverUsernameXml("Adam-Kahtava");
            }
            catch(Exception)
            {
                Assert.Fail("WebDev.WebServer.exe (cassini) probably isn't running, try starting up the ServiceHost project.");
            }
        }

        [Test]
        public void ShouldVerifyProxyAndReturnReviews()
        {
            var amazonService = new AmazonService();

            var reviewsFromXmlRequest = amazonService.ReviewsByCustomerIdXml("A2JM0EQJELFL69");

            Assert.IsNotNull(reviewsFromXmlRequest);
            Assert.Greater(reviewsFromXmlRequest.Count, 1);
            
            var reviewsFromJsonRequest = amazonService.ReviewsByCustomerIdJson("A2JM0EQJELFL69");

            Assert.IsNotNull(reviewsFromJsonRequest);
            Assert.AreEqual(reviewsFromXmlRequest.Count, reviewsFromJsonRequest.Count);
        }

        [Test]
        public void ShouldVerifyProxyAndReturnWishlist()
        {
            var amazonService = new AmazonService();

            var wishlistFromXmlRequest = amazonService.WishlistByListIdXml("3JU6ASKNUS7B8");

            Assert.IsNotNull(wishlistFromXmlRequest);
            Assert.Greater(wishlistFromXmlRequest.Count, 1);

            var wishlistFromJsonRequest = amazonService.WishlistByListIdXml("3JU6ASKNUS7B8");

            Assert.IsNotNull(wishlistFromJsonRequest);
            Assert.AreEqual(wishlistFromXmlRequest.Count, wishlistFromJsonRequest.Count);
        }

        [Test]
        public void ShouldVerifyProxyAndReturnProfile()
        {
            var amazonService = new AmazonService();

            var profileFromXmlRequest = amazonService.DiscoverUsernameXml("Adam-Kahtava");

            Assert.IsNotNull(profileFromXmlRequest);
            Assert.IsFalse(string.IsNullOrEmpty(profileFromXmlRequest.CustomerId));
            Assert.IsFalse(string.IsNullOrEmpty(profileFromXmlRequest.ListId));

            var profileFromJsonRequest = amazonService.DiscoverUsernameJson("Adam-Kahtava");

            Assert.AreEqual(profileFromXmlRequest.CustomerId, profileFromJsonRequest.CustomerId);
            Assert.AreEqual(profileFromXmlRequest.ListId, profileFromJsonRequest.ListId);
        }
    }
}
