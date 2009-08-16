using System.Diagnostics;
using AdamDotCom.Amazon.Service;
using NUnit.Framework;

namespace Unit.Tests
{
    public class AmazonProfileSnifferTests
    {
        [TestFixture]
        public class AmazonServiceTests
        {
            [Test]
            public void ShouldVerifyCustomerIdCanBeFoundMultipleResults()
            {
                var profileSniffer = new ProfileSniffer("Adam Kahtava");
                Assert.AreEqual("A2JM0EQJELFL69", profileSniffer.GetCustomerId());
            }

            [Test]
            public void ShouldVerifyWishListIdCanBeFoundFromMultipleResults()
            {
                var profileSniffer = new ProfileSniffer("Adam Kahtava");
                Assert.AreEqual("3JU6ASKNUS7B8", profileSniffer.GetListId());
            }

            [Test]
            public void ShouldVerifyWishListIdCanBeFoundSingleResult()
            {
                var profileSniffer = new ProfileSniffer("Joel Spolsky");
                Assert.AreEqual("AC49KE006R2ZU", profileSniffer.GetCustomerId());
                Assert.AreEqual("1RVDGPM8SWXG4", profileSniffer.GetListId());
            }

            [Test]
            public void ShouldReturnErrorsWhenUserNotFound()
            {
                var profileSniffer = new ProfileSniffer("gonzo the great and cookie monster");
                var listId = profileSniffer.GetListId();
                var customerId = profileSniffer.GetCustomerId();

                foreach (var error in profileSniffer.Errors)
                {
                    Debug.WriteLine(error);
                }

                Assert.AreEqual(1, profileSniffer.Errors.Count);
            }

            [Test]
            public void ShouldReturnErrorsWhenPageSourceNotFound()
            {
                var profileSniffer = new ProfileSniffer();
                
                var listId = profileSniffer.GetListId();
                var customerId = profileSniffer.GetCustomerId();

                foreach (var error in profileSniffer.Errors)
                {
                    Debug.WriteLine(error);
                }

                Assert.AreEqual(2, profileSniffer.Errors.Count);
            }
        }
    }
}