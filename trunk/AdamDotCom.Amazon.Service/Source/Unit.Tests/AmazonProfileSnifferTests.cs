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
            public void ShouldVerifyProfileCanBeFoundMultipleResults()
            {
                var profileSniffer = new ProfileSniffer("Adam Kahtava");
                var profile = profileSniffer.GetProfile();

                Assert.AreEqual("A2JM0EQJELFL69", profile.CustomerId);
                Assert.AreEqual("3JU6ASKNUS7B8", profile.ListId);
            }

            [Test]
            public void ShouldVerifyProfileCanBeFoundMultipleResultsWithSpecialCharacters()
            {
                var profileSniffer = new ProfileSniffer("Adam-Kahtava");
                var profile = profileSniffer.GetProfile();

                Assert.AreEqual("A2JM0EQJELFL69", profile.CustomerId);
                Assert.AreEqual("3JU6ASKNUS7B8", profile.ListId);

                profileSniffer = new ProfileSniffer("Adam%20Kahtava");
                profile = profileSniffer.GetProfile();

                Assert.AreEqual("A2JM0EQJELFL69", profile.CustomerId);
                Assert.AreEqual("3JU6ASKNUS7B8", profile.ListId);
            }

            [Test]
            public void ShouldVerifyProfileCanBeFoundSingleResult()
            {
                var profileSniffer = new ProfileSniffer("Joel Spolsky");
                var profile = profileSniffer.GetProfile();

                Assert.AreEqual("AC49KE006R2ZU", profile.CustomerId);
                Assert.AreEqual("1RVDGPM8SWXG4", profile.ListId);
            }

            [Test]
            public void ShouldReturnErrorsWhenUserNotFound()
            {
                var profileSniffer = new ProfileSniffer("gonzo the great and cookie monster");
                var profile = profileSniffer.GetProfile();

                Debug.WriteLine(profile.CustomerId);
                Debug.WriteLine(profile.ListId);

                foreach (var error in profileSniffer.Errors)
                {
                    Debug.WriteLine(error);
                }

                Assert.AreEqual(2, profileSniffer.Errors.Count);
            }

            [Test]
            public void ShouldReturnErrorsWhenPageSourceNotFound()
            {
                var profileSniffer = new ProfileSniffer();
                profileSniffer.GetProfile();
                
                foreach (var error in profileSniffer.Errors)
                {
                    Debug.WriteLine(error);
                }

                Assert.AreEqual(2, profileSniffer.Errors.Count);
            }
        }
    }
}